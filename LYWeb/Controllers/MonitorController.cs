using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Models;
using System.IO;
using PagedList;
using LYWeb.Models.MetaData;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class MonitorController : Controller
    {

        private TudsEntities db = new TudsEntities();


        public ActionResult Index(int? page)
        {
            if (Session["isRemindLatestTrain"] == null)
            {
                Session["isRemindLatestTrain"] = "T";
            }
            DateTime time = DateTime.Now.AddHours(-48).AddYears(-1);
            IQueryable<TRAIN> trains = db.TRAIN.Where(t => t.DETECTION_TIME >= time).OrderByDescending(t => t.DETECTION_TIME).Include(t => t.SITES).Include(t => t.SITES);


            var notAlertedTrains = trains.Where(t => t.ALERT_PLAYED == false);
            if(notAlertedTrains.Count()>0)
            {
                ViewBag.PlayAlert = true;
                foreach(var item in notAlertedTrains)
                {
                    item.ALERT_PLAYED = true;
                }
                db.SaveChanges();
            }
            else
            {
                ViewBag.PlayAlert = false;
            }
            //马天：合入下面这个region
            #region 合入
            IQueryable<TRAIN> tainsForDeal = trains.Take(50);
            List<TRAIN> undealedTrain = new List<TRAIN>();
            foreach(TRAIN t in tainsForDeal)
            {
                var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == t.ID);
                if(faults.Count() == 0)
                {
                    continue;
                }
                var unreckecked = faults.Where(f => f.ISRECHECKED == false);
                if (unreckecked.Count() > 0)
                {
                    undealedTrain.Add(t);                    
                }                
            }
            if(undealedTrain.Count()==0)
            {
                ViewBag.Undealed = "";
            }
            else
            {
                string undealed = "";
                foreach (var t in undealedTrain)
                {
                    undealed = undealed + t.DETECTION_TIME.ToString() + "" + t.NAME + ",";
                }
                undealed = undealed + "列车的报警未处理完成";
                ViewBag.Undealed = undealed;
            }
            #endregion

            //报警提示
            //DateTime alarmtime = DateTime.Now.AddDays(-100);
            //var toAlarmTrains = db.TRAIN.Where(t => t.DETECTION_TIME >= alarmtime).ToList();
            //AlarmTrainsToSession(toAlarmTrains);


            //调车过车的ID
            //var shunting = (from tr in trains.Where(t => t.AEI_CONTAINED == true && t.INSPECTION_CONTAINED == false && t.SCRAPE_CONTAINED == false && t.DIMENSION_CONTAINED == false) select tr.ID).ToList();
            //trains = trains.Where(t => !shunting.Contains(t.ID));
            int pageNumber = page ?? 1;
            var onePageTrain = trains.ToPagedList(pageNumber, 15);
            
            return View(onePageTrain);
        }
        public ActionResult Fault()
        {
            var fault = db.FAULT.Include(f => f.WHEEL).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            return View(fault.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Fault(string submit, string SUGGESTION, HttpPostedFileBase file)
        {
            if(submit.StartsWith("suggestion"))
            {
                submit = submit.Replace("suggestion","");
                try
                {
                    RECHECKS recheck = new RECHECKS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        FAULT_ID = submit,
                        SUGGESTION = SUGGESTION,
                        HANDLER = Session["realname"].ToString(),
                        DEALT_TIME = DateTime.Now,
                    };
                    FAULT fault = db.FAULT.Find(submit);
                    if (SUGGESTION == "暂不处理")
                    {
                        recheck.STATE = "已处理";
                        fault.ISRECHECKED = true;
                        fault.IS_COMPLETED = true;
                    }
                    else if (SUGGESTION == "跟踪控制")
                    {
                        recheck.STATE = "已处理";
                        fault.ISRECHECKED = true;
                        fault.IS_COMPLETED = true;
                    }
                    else if (SUGGESTION == "复查判断")
                    {
                        recheck.STATE = "处理中";
                        fault.ISRECHECKED = true;
                        //fault.IS_COMPLETED = false;
                        fault.IS_COMPLETED = true;
                    }
                    else
                    {
                        return Fault();
                    }
                    db.RECHECKS.Add(recheck);
                    db.SaveChanges();
                    //if (SUGGESTION == "复查判断")
                    //{
                    //    if (fault.ITEM == "同转向架轮径差" || fault.ITEM == "同车轮径差" || fault.ITEM == "同轴轮径差")
                    //    {
                    //        return RedirectToAction("Edit2", "Fault", new { id = fault.ID });
                    //        //return Redirect($"/Fault/Edit2/{fault.ID}");
                    //    }
                    //    else
                    //    {
                    //        //return Redirect($"/Fault/Edit/{fault.ID}");
                    //        return RedirectToAction("Edit", "Fault", new { id = fault.ID });
                    //    }

                    //}
                }
                catch
                {
                    //log here
                }
            }
            else
            {
                //unexpected
            }
            return Fault();
        }

        private void AlarmTrainsToSession(List<TRAIN> trains)
        {
            if (trains == null || trains.Count == 0)
            {
                return;
            }
            List<AlarmTrain> existingTrains = null;
            try
            {
                existingTrains = (List<AlarmTrain>)Session["alarmTrains"];
                Session.Remove("alarmTrains");
            }
            catch (Exception ex)
            {

            }
            if (existingTrains == null)
            {
                existingTrains = new List<AlarmTrain>();
            }

            foreach (TRAIN train in trains)
            {
                //如果车需要报警，将其放入session
                if (IsAlarmNeeded(train.ID))
                {
                    AlarmTrain aNewAlarmTrain = new AlarmTrain(train);
                    if (-1 == existingTrains.FindIndex(t => t.ID == train.ID))
                    {
                        existingTrains.Add(aNewAlarmTrain);
                    }

                }
                //如果不需要报警，从session中移除（如有）
                else
                {
                    existingTrains.RemoveAll(t => t.ID == train.ID);
                }
            }
            if (existingTrains.Count != 0)
            {
                Session.Add("alarmTrains", existingTrains);
            }
        }

        /// <summary>
        /// 本方法参照HtmlHelperExtensions.cs中DisplayFaultLinkForTrain方法写的
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool IsAlarmNeeded(string id)
        {
            TudsEntities db = new TudsEntities();
            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id);
            if (faults.Count() == 0)
            {
                return false;
            }
            var unreckecked = faults.Where(f => f.ISRECHECKED == false);
            if (unreckecked.Count() > 0)
            {
                if (unreckecked.Where(f => f.ALARM_LEVEL == 3).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                var onProcessed = faults.Where(f => f.IS_COMPLETED == false);
                if (onProcessed.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public void DeleteFromAlarmSessionByIds(string deleteIds)
        {
            string[] idArray = deleteIds.Split('|');
            List<AlarmTrain> existingTrains = null;
            try
            {
                existingTrains = (List<AlarmTrain>)Session["alarmTrains"];
                Session.Remove("alarmTrains");
            }
            catch (Exception ex)
            {
                return;
            }
            if (existingTrains == null)
            {
                return;
            }
            foreach (string id in idArray)
            {
                existingTrains.ForEach(t => t.IsAlarmed = (t.ID == id ? true : t.IsAlarmed));
            }
            if (existingTrains.Count != 0)
            {
                Session.Add("alarmTrains", existingTrains);
            }
        }

        public string GetAlertSession()
        {
            if (Session["isRemindLatestTrain"] == null)
            {
                Session["isRemindLatestTrain"] = "T";
            }
            string result = Session["isRemindLatestTrain"].ToString();
            return result;
        }

        public void ChangeAlertSession(string alert)
        {
            Session["isRemindLatestTrain"] = alert;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
