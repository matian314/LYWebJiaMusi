using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Helpers;
using LYWeb.Models;
using NPOI.SS.Formula.Functions;
using PagedList;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class FaultController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: Fault
        public ActionResult Index(string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem, int? page)
        {
            StartTime = StartTime ?? DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd ") + "18:00:00";
            EndTime = EndTime ?? DateTime.Now.ToString("yyyy-MM-dd ") + "18:00:00";
            AlarmLevel = AlarmLevel ?? "123";
            DataSource = DataSource ?? "ALARM_LEVEL";
            FaultItem = FaultItem ?? "ALARM_LEVEL";
            DateTime start;
            DateTime end;
            var faults = (IEnumerable<FAULT>)db.FAULT;
            try
            {
                start = DateTime.Parse(StartTime);
                end = DateTime.Parse(EndTime);
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME >= start);
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME <= end);
            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
            {
                faults = faults.OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            }
            else
            {
                faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.NAME.EndsWith(CarNumber)).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            }
            faults = SelectListHelper.GetFaultByAlarmLevel(faults, AlarmLevel, DataSource, FaultItem);
            int pageNumber = page ?? 1;
            var onePageFault = faults.ToPagedList(pageNumber, 15);
            ViewBag.CarNumber = CarNumber;
            ViewBag.StartTime = StartTime;
            ViewBag.EndTime = EndTime;
            ViewBag.AlarmLevel = AlarmLevel;
            ViewBag.DataSource = DataSource;
            ViewBag.FaultItem = FaultItem;
            return View(onePageFault);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string submit, string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem, int? page)
        {
            //Query按钮被按下，返回查询结果
            if (submit == "查询")
            {
                DateTime start;
                DateTime end;
                var faults = (IEnumerable<FAULT>)db.FAULT;
                try
                {
                    start = DateTime.Parse(StartTime);
                    end = DateTime.Parse(EndTime);
                }
                catch (Exception)
                {
                    start = DateTime.MinValue;
                    end = DateTime.MaxValue;
                }
                faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME >= start);
                faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME <= end);
                if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
                {
                    faults = faults.OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
                }
                else
                {
                    faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.NAME.EndsWith(CarNumber)).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
                }
                faults = SelectListHelper.GetFaultByAlarmLevel(faults, AlarmLevel, DataSource, FaultItem);
                int pageNumber = page ?? 1;
                var onePageFault = faults.ToPagedList(pageNumber, 15);
                ViewBag.CarNumber = CarNumber;
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ViewBag.AlarmLevel = AlarmLevel;
                ViewBag.DataSource = DataSource;
                ViewBag.FaultItem = FaultItem;
                return View(onePageFault);
            }
            //增添Rechecks表
            else
            {
                string SUGGESTION = Request.Form[$"SUGGESTION{submit}"]?.ToString();
                string handlerName = Request.Form[$"HANDLER{submit}"]?.ToString();
                
                if (string.IsNullOrEmpty(handlerName) || string.IsNullOrWhiteSpace(handlerName))
                {
                    //handlerName = Session["realname"].ToString();
                    handlerName = "值班员";
                }
                if (SUGGESTION != "暂不处理" && SUGGESTION != "跟踪控制" && SUGGESTION != "复查判断")
                {
                    //??????
                    ViewBag.CarNumber = CarNumber;
                    ViewBag.StartTime = StartTime;
                    ViewBag.EndTime = EndTime;
                    ViewBag.AlarmLevel = AlarmLevel;
                    ViewBag.DataSource = DataSource;
                    ViewBag.FaultItem = FaultItem;
                    return Index(CarNumber, StartTime, EndTime, AlarmLevel, DataSource, FaultItem, 1);
                }
                try
                {
                    RECHECKS recheck = new RECHECKS()
                    {
                        ID = Guid.NewGuid().ToString(),
                        FAULT_ID = submit,
                        SUGGESTION = SUGGESTION,
                        HANDLER = handlerName,
                        DEALT_TIME = DateTime.Now,
                    };
                    FAULT fault = db.FAULT.Find(submit);

                    recheck.STATE = "已处理";
                    fault.ISRECHECKED = true;
                    fault.IS_COMPLETED = true;

                    //佳木斯机务段不回填，复查直接就已处理

                    //if (SUGGESTION == "暂不处理")
                    //{
                    //    recheck.STATE = "已处理";
                    //    fault.ISRECHECKED = true;
                    //    fault.IS_COMPLETED = true;
                    //}
                    //else if (SUGGESTION == "跟踪控制")
                    //{
                    //    recheck.STATE = "已处理";
                    //    fault.ISRECHECKED = true;
                    //    fault.IS_COMPLETED = true;
                    //}
                    //else//SUGGESTION == "复查判断"
                    //{
                    //    recheck.STATE = "处理中";
                    //    fault.ISRECHECKED = true;
                    //    fault.IS_COMPLETED = false;
                    //}
                    db.RECHECKS.Add(recheck);
                    db.SaveChanges();
                    //if(SUGGESTION == "复查判断")
                    //{
                    //    if(fault.ITEM == "同转向架轮径差" || fault.ITEM == "同车轮径差" || fault.ITEM == "同轴轮径差")
                    //    {
                    //        return RedirectToAction("Edit2", "Fault",new { id = fault.ID });
                    //    }
                    //    else
                    //    {
                    //        return RedirectToAction("Edit", "Fault", new { id = fault.ID});
                    //    }
                    //}
                }
                catch
                {
                    //log here
                }
                ViewBag.CarNumber = CarNumber;
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ViewBag.AlarmLevel = AlarmLevel;
                ViewBag.DataSource = DataSource;
                ViewBag.FaultItem = FaultItem;
                return Index(CarNumber, StartTime, EndTime, AlarmLevel, DataSource, FaultItem, 1);

            }
        }

        public ActionResult Train(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var Train = db.TRAIN.Find(id);
            if (Train == null)
            {
                return View(new List<FAULT>());
            }
            ViewBag.Train = Train;
            var fault = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id).Include(f => f.WHEEL).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            return View(fault.ToList());
        }
        [HttpPost]
        public ActionResult Train(string id, string submit)
        {
            string SUGGESTION = Request.Form[$"SUGGESTION{submit}"]?.ToString();
            string handlerName = Request.Form[$"HANDLER{submit}"]?.ToString();
            if (SUGGESTION != "暂不处理" && SUGGESTION != "跟踪控制" && SUGGESTION != "复查判断")
            {
                return Train(id);
            }
            if (string.IsNullOrEmpty(handlerName) || string.IsNullOrWhiteSpace(handlerName))
            {
                //handlerName = Session["realname"].ToString();
                handlerName = "值班员";
            }
            try
            {
                RECHECKS recheck = new RECHECKS()
                {
                    ID = Guid.NewGuid().ToString(),
                    FAULT_ID = submit,
                    FAULT = db.FAULT.Find(submit),
                    SUGGESTION = SUGGESTION,                    
                    HANDLER = handlerName,
                    DEALT_TIME = DateTime.Now,
                };
                FAULT fault = db.FAULT.Find(submit);
                //佳木斯机务段不回填数据，都是已处理
                recheck.STATE = "已处理";
                fault.ISRECHECKED = true;
                fault.IS_COMPLETED = true;

                //if (SUGGESTION == "暂不处理")
                //{
                //    recheck.STATE = "已处理";
                //    fault.ISRECHECKED = true;
                //    fault.IS_COMPLETED = true;
                //}
                //else if (SUGGESTION == "跟踪控制")
                //{
                //    recheck.STATE = "已处理";
                //    fault.ISRECHECKED = true;
                //    fault.IS_COMPLETED = true;
                //}
                //else if (SUGGESTION == "复查判断")
                //{
                //    recheck.STATE = "处理中";
                //    fault.ISRECHECKED = true;
                //    fault.IS_COMPLETED = false;
                //}
                //else
                //{
                //    return Train(id);
                //}
                db.RECHECKS.Add(recheck);
                db.SaveChanges();
                //if (SUGGESTION == "复查判断")
                //{
                //    if (fault.ITEM == "同转向架轮径差" || fault.ITEM == "同车轮径差" || fault.ITEM == "同轴轮径差")
                //    {
                //        return RedirectToAction("Edit2", "Fault", new { id = fault.ID });
                //    }
                //    else
                //    {
                //        return RedirectToAction("Edit", "Fault", new { id = fault.ID });
                //    }
                //}
            }
            catch
            {
                //log here
            }
            return Train(id);
        }
        //仅适用于擦伤，探伤，几何尺寸中的单个数值项目的复核
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return View();
            }
            var recheck = fault.RECHECKS.FirstOrDefault();
            if (recheck == null)
            {
                //没有rechek
                return Train(fault.WHEEL.VEHICLE.TRAIN_ID);
            }
            ViewBag.Wheel = fault.WHEEL;
            return View(recheck);
        }
        //仅适用于擦伤，探伤，几何尺寸中的单个数值项目的复核
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(string id, [Bind(Include = "ID,FAULT_ID,CONCLUSION,VALUE,RECHECK_PERSON,RECHECKED_TIME,TEAM_NAME,ERROR_ANALYSIS,RECHECK_HANDLER,FOREMAN,QUALITY_INSPECTOR,DISPATCHER")]RECHECKS recheck)
        {
            FAULT fault = db.FAULT.Find(id);
            RECHECKS oldRecheck = fault.RECHECKS.FirstOrDefault();
            if (ModelState.IsValid)
            {
                fault.IS_COMPLETED = true;
                oldRecheck.CONCLUSION = recheck.CONCLUSION;
                oldRecheck.VALUE = recheck.VALUE;
                oldRecheck.RECHECK_PERSON = recheck.RECHECK_PERSON;
                oldRecheck.RECHECKED_TIME = recheck.RECHECKED_TIME;
                oldRecheck.TEAM_NAME = recheck.TEAM_NAME;
                oldRecheck.ERROR_ANALYSIS = recheck.ERROR_ANALYSIS;
                oldRecheck.RECHECK_HANDLER = recheck.RECHECK_HANDLER;
                oldRecheck.FOREMAN = recheck.FOREMAN;
                oldRecheck.QUALITY_INSPECTOR = recheck.QUALITY_INSPECTOR;
                oldRecheck.DISPATCHER = recheck.DISPATCHER;

                oldRecheck.STATE = "已处理";
                fault.IS_COMPLETED = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Fault", null);
            }
            else
            {
                recheck.FAULT = fault;
                return View(recheck);
            }
        }
        //适用于几何尺寸中的多个数值项目的复核
        public ActionResult Edit2(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return View();
            }
            var recheck = fault.RECHECKS.FirstOrDefault();
            if (recheck == null)
            {
                //没有rechek
                return Train(fault.WHEEL.VEHICLE.TRAIN_ID);
            }
            ViewBag.Wheel = fault.WHEEL;
            return View(recheck);
        }
        //适用于几何尺寸中的多个数值项目的复核
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit2(string id, [Bind(Include = "ID,FAULT_ID,CONCLUSION,VALUE1,VALUE2,VALUE3,VALUE4,VALUE5,VALUE6,VALUE7,VALUE8,DIFF_VALUE,RECHECK_PERSON,RECHECKED_TIME,TEAM_NAME,ERROR_ANALYSIS,RECHECK_HANDLER,FOREMAN,QUALITY_INSPECTOR,DISPATCHER")]RECHECKS recheck)
        {
            FAULT fault = db.FAULT.Find(id);
            RECHECKS oldRecheck = fault.RECHECKS.FirstOrDefault();
            if (ModelState.IsValid)
            {
                fault.IS_COMPLETED = true;
                oldRecheck.CONCLUSION = recheck.CONCLUSION;
                oldRecheck.VALUE1 = recheck.VALUE1;
                oldRecheck.VALUE2 = recheck.VALUE2;
                oldRecheck.VALUE3 = recheck.VALUE3;
                oldRecheck.VALUE4 = recheck.VALUE4;
                oldRecheck.VALUE5 = recheck.VALUE5;
                oldRecheck.VALUE6 = recheck.VALUE6;
                oldRecheck.VALUE7 = recheck.VALUE7;
                oldRecheck.VALUE8 = recheck.VALUE8;
                oldRecheck.DIFF_VALUE = recheck.DIFF_VALUE;
                oldRecheck.VALUE = recheck.DIFF_VALUE?.ToString();
                oldRecheck.RECHECK_PERSON = recheck.RECHECK_PERSON;
                oldRecheck.RECHECKED_TIME = recheck.RECHECKED_TIME;
                oldRecheck.TEAM_NAME = recheck.TEAM_NAME;
                oldRecheck.ERROR_ANALYSIS = recheck.ERROR_ANALYSIS;
                oldRecheck.RECHECK_HANDLER = recheck.RECHECK_HANDLER;
                oldRecheck.FOREMAN = recheck.FOREMAN;
                oldRecheck.QUALITY_INSPECTOR = recheck.QUALITY_INSPECTOR;
                oldRecheck.DISPATCHER = recheck.DISPATCHER;
                oldRecheck.STATE = "已处理";
                fault.IS_COMPLETED = true;
                db.SaveChanges();
                return RedirectToAction("Index", "Fault", null);
            }
            else
            {
                recheck.FAULT = fault;
                return View(recheck);
            }
        }
        //仅适用于擦伤，探伤，几何尺寸中的单个数值项目的复核
        public ActionResult Show(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return View();
            }
            var recheck = fault.RECHECKS.FirstOrDefault();
            if (recheck == null)
            {
                //没有rechek
                return Train(fault.WHEEL.VEHICLE.TRAIN_ID);
            }
            ViewBag.Wheel = fault.WHEEL;
            return View(recheck);
        }
        //适用于几何尺寸中的多个数值项目的复核
        public ActionResult Show2(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return View();
            }
            var recheck = fault.RECHECKS.FirstOrDefault();
            if (recheck == null)
            {
                //没有rechek
                return Train(fault.WHEEL.VEHICLE.TRAIN_ID);
            }
            ViewBag.Wheel = fault.WHEEL;
            return View(recheck);
        }
        public ActionResult UpLoad(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            FAULT fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fault = fault;
            var result = db.FAULT_IMAGE.Where(f => f.FAULT_ID == fault.ID);
            return View(result.ToList());
        }
        [HttpPost]
        public ActionResult UpLoad(string id, HttpPostedFileBase file)
        {
            if (file == null)
            {
                return UpLoad(id);
            }
            FAULT fault = db.FAULT.Find(id);
            ViewBag.Fault = fault;
            if (file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".jpeg") || file.FileName.EndsWith(".bmp") || file.FileName.EndsWith(".png") || file.FileName.EndsWith(".gif"))
            {
                try
                {
                    string dir = Server.MapPath($"/Tuds报警图片/{id}");
                    
                    Directory.CreateDirectory(dir);
                    string name = file.FileName.Substring(file.FileName.LastIndexOf('\\') + 1);
                    string path = Path.Combine(dir, name);

                    file.SaveAs(path);
                    FAULT_IMAGE image = new FAULT_IMAGE()
                    {
                        ID = Guid.NewGuid().ToString(),
                        FAULT_ID = id,
                        PATH = $"/Tuds报警图片/{id}/{name}"
                    };
                    db.FAULT_IMAGE.Add(image);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    //log here
                    return UpLoad(id);
                }
                return UpLoad(id);
            }
            else
            {
                //无效的格式
                return UpLoad(id);
            }
        }

        public ActionResult UpLoadShow(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            FAULT fault = db.FAULT.Find(id);
            if (fault == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fault = fault;
            var result = db.FAULT_IMAGE.Where(f => f.FAULT_ID == fault.ID);
            return View(result.ToList());
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
