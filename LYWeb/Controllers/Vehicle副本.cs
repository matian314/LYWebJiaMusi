using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LYWeb.Helpers;
using LYWeb.Models;
using NPOI.SS.Formula.Functions;
using PagedList;

namespace TudsWeb.Controllers
{
    //[Authorize]
    public class VehicleController : Controller
    {
        private TudsEntities db = new TudsEntities();

        //public ActionResult Index(int? page)
        //{
        //    var pageNumber = page ?? 1;

        //    DateTime time = DateTime.Now.AddMonths(-1);
        //    var train = db.TRAIN.Where(t => (t.DETECTION_TIME >= time)).OrderByDescending(t => t.DETECTION_TIME).Include(t => t.SITES).Include(t => t.SITES).ToList();

        //    var onePageTrain = train.ToPagedList(pageNumber, 15);

        //    ViewBag.CarNumber = "";
        //    ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00"; ;
        //    ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
        //    ViewBag.AlarmLevel = "123";
        //    ViewBag.DataSource = "ALARM_LEVEL";
        //    ViewBag.FaultItem = "ALARM_LEVEL";
        //    return View(onePageTrain);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem, int? page)
        {
            if(string.IsNullOrEmpty(CarNumber) && string.IsNullOrEmpty(StartTime) && string.IsNullOrEmpty(EndTime) && string.IsNullOrEmpty(AlarmLevel) && string.IsNullOrEmpty(DataSource) && string.IsNullOrEmpty(FaultItem))
            {
                CarNumber = "";
                StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00"; ;
                EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
                AlarmLevel = "123";
                DataSource = "ALARM_LEVEL";
                FaultItem = "ALARM_LEVEL";
            }
            DateTime start;
            DateTime end;
            var trains = (IEnumerable<TRAIN>)db.TRAIN;
            if(string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4)
            {
                //不处理
                try
                {
                    trains = trains.OrderByDescending(t => t.DETECTION_TIME);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("fail", "string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4" + ex.Message);
                    return View();
                }
                
            }
            else
            {
                try
                {
                    CarNumber = CarNumber.Trim();
                    trains = trains.Where(t => t.NAME.EndsWith(CarNumber));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("fail", "NOT string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4" + ex.Message);
                    return View();
                }


                
            }
            try
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    start = DateTime.Parse(StartTime);
                    trains = trains.Where(t => t.DETECTION_TIME >= start);
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    end = DateTime.Parse(EndTime);
                    trains = trains.Where(t => t.DETECTION_TIME <= end);
                }
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
                ModelState.AddModelError("fail", "时间格式错误");
            }
            try
            {
                if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4)
                {
                    //不处理
                    trains = trains.OrderByDescending(t => t.DETECTION_TIME);
                }
                else
                {
                    CarNumber = CarNumber.Trim();
                    ModelState.AddModelError("carnum", CarNumber);
                    trains = trains.Where(t => t.NAME.EndsWith(CarNumber)).OrderByDescending(t => t.DETECTION_TIME);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("fail", ex.Source);
            }
            

                trains = SelectListHelper.GetTrainByAlarmLevel(trains, AlarmLevel, DataSource, FaultItem);

            //调车过车的ID
            //List<string> shunting = (from tr in trains.Where(t => t.AEI_CONTAINED == true && t.INSPECTION_CONTAINED == false && t.SCRAPE_CONTAINED == false && t.DIMENSION_CONTAINED == false) select tr.ID).ToList();
            //trains = trains.Where(t => !shunting.Contains(t.ID));

            var pageNumber = page ?? 1;
            var onePageTrain = trains.ToPagedList(pageNumber, 15);

            ViewBag.CarNumber = CarNumber;
            ViewBag.StartTime = StartTime;
            ViewBag.EndTime = EndTime;
            ViewBag.AlarmLevel = AlarmLevel;
            ViewBag.DataSource = DataSource;
            ViewBag.FaultItem = FaultItem;
            return View(onePageTrain);
        }

        public ActionResult WheelPartial(string id)
        {
            var wheels = db.WHEEL.Where(w => w.VEHICLE_ID == id).OrderBy(w => w.POSITION);
            return PartialView(wheels.ToList());
        }

        public ActionResult Dimension(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var TRAIN = db.TRAIN.Find(id);
            if(TRAIN is null)
            {
                return View(new List<VEHICLE>());
            }
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == id).Include(v => v.WHEEL).Include(v => v.TRAIN).OrderBy(v => v.PASS_ORDER);
            ViewBag.Train = TRAIN;
            return View(vehicles.ToList());
        }

        public ActionResult Scrape(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var TRAIN = db.TRAIN.Find(id);
            if (TRAIN is null)
            {
                return View(new List<VEHICLE>());
            }
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == id).Include(v => v.WHEEL).Include(v => v.TRAIN).OrderBy(v => v.PASS_ORDER);
            ViewBag.Train = TRAIN;
            return View(vehicles.ToList());
        }

        public ActionResult Inspection(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var TRAIN = db.TRAIN.Find(id);
            if (TRAIN is null)
            {
                return View(new List<VEHICLE>());
            }
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == id).Include(v => v.WHEEL).Include(v => v.TRAIN).OrderBy(v => v.PASS_ORDER);
            ViewBag.Train = TRAIN;
            return View(vehicles.ToList());
        }

        public ActionResult UnitRecheck(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            var train = db.TRAIN.Find(id);
            if (train is null)
            {
                return View();
            }
            return View(train);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnitRecheck(string id, string submit, string EndPosition)
        {
            if(submit == "提交")
            {
                if (string.IsNullOrEmpty(id))
                {
                    return HttpNotFound();
                }
                var train = db.TRAIN.Find(id);
                if (train == null)
                {
                    return HttpNotFound();
                }
                train.NAME = Request.Form["NAME"].ToString().Trim();
                train.UNIT_NUMBER1 = train.NAME;
                train.END_POSITION = EndPosition;
                string name = train.NAME;
                var unit = db.UNIT_NUMBER_DICT.Where(u => u.UNIT_NUMBER == name).FirstOrDefault();
                if (unit != null)
                {
                    train.DEPOT = unit.DEPOT_NAME;
                }
                foreach (var vehicle in train.VEHICLE)
                {
                    string coachNumber = Request.Form[vehicle.ID].ToString().Trim();
                    vehicle.NAME = name + " " + coachNumber;
                    vehicle.UNIT_NUMBER = name;
                    foreach(var wheel in vehicle.WHEEL)
                    {
                        wheel.NAME = vehicle.NAME + wheel.NAME.Substring(wheel.NAME.Length - 3);
                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    //log here
                    ViewBag.Tips = "录入失败";
                    return View();
                }
                return RedirectToAction("Index", "Vehicle", null);
            }
            else
            {
                return UnitRecheck(id);
            }
        }

        public ActionResult Data(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            TRAIN train = db.TRAIN.Find(id);
            if(train == null)
            {
                return HttpNotFound();
            }
            ViewBag.Train = train;
            TRAIN lastTrain = null;
            try
            {
                lastTrain = db.TRAIN.Where(t => t.NAME == train.NAME && t.DETECTION_TIME < train.DETECTION_TIME).OrderByDescending(t => t.DETECTION_TIME).First();
            }
            catch(Exception ex)
            {
                //do nothing
            }
            ViewBag.LastTrain = lastTrain;
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == train.ID).OrderBy(v => v.PASS_ORDER);
            return View(vehicles.ToList());
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
