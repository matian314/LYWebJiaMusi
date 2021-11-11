using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

            if (string.IsNullOrEmpty(CarNumber) && string.IsNullOrEmpty(StartTime) && string.IsNullOrEmpty(EndTime) && string.IsNullOrEmpty(AlarmLevel) && string.IsNullOrEmpty(DataSource) && string.IsNullOrEmpty(FaultItem))
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
                //不处理
                


                
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

            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4)
            {
                //不处理
                trains = trains.OrderByDescending(t => t.DETECTION_TIME);
            }
            else
            {
                CarNumber = CarNumber.Trim();
                trains = trains.Where(t => !string.IsNullOrEmpty(t.NAME) && t.NAME.EndsWith(CarNumber)).OrderByDescending(t => t.DETECTION_TIME);
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
            GetAEIPhotos(train);
            return View(train);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnitRecheck(string id, string submit, string EndPosition,string CARTYPE, string CARNUMBER)
        {
            if(submit == "提交")
            {
                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(CARTYPE) || string.IsNullOrEmpty(CARNUMBER))
                {
                    return HttpNotFound();
                }
                var train = db.TRAIN.Find(id);
                if (train == null)
                {
                    return HttpNotFound();
                }
                bool isReverseWheels = EndPosition == train.END_POSITION ? false:true;
                
                string name = CARTYPE.Trim().ToUpper() + "-" + CARNUMBER.Trim();
                //train.NAME = Request.Form["NAME"].ToString().Trim();
                train.NAME = name;
                train.UNIT_NUMBER1 = name;
                train.END_POSITION = EndPosition;
                //string name = train.NAME;
                var unit = db.UNIT_NUMBER_DICT.Where(u => u.UNIT_NUMBER == name).FirstOrDefault();
                if (unit != null)
                {
                    train.DEPOT = unit.DEPOT_NAME;
                }
                foreach (var vehicle in train.VEHICLE)
                {                    
                    vehicle.NAME = name;
                    vehicle.UNIT_NUMBER = name;
                    vehicle.COACH_NUMBER = name;
                    //var car_type = db.CAR_TYPE.Where(t => t.TYPE == CARTYPE);
                    vehicle.CAR_TYPE1 = db.CAR_TYPE.Where(t => t.TYPE == CARTYPE).FirstOrDefault();
                    vehicle.AEI_END_DIRECTION = EndPosition;
                    foreach (var wheel in vehicle.WHEEL)
                    {
                        if(isReverseWheels)
                        {                                                    
                            wheel.AXLE_POSITION = (byte)(7 - wheel.AXLE_POSITION);
                            if(wheel.POSITION.Contains("左"))
                            {
                                wheel.POSITION = $"右{wheel.AXLE_POSITION}";
                            }
                            else if(wheel.POSITION.Contains("右"))
                            {
                                wheel.POSITION = $"左{wheel.AXLE_POSITION}";
                            }                            
                        }
                        wheel.NAME = name + " " + wheel.POSITION;

                    }
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    //log here
                    ViewBag.Tips = "录入失败";
                    return View(train);
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
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            TRAIN train = db.TRAIN.Find(id);
            if (train == null)
            {
                return HttpNotFound();
            }
            ViewBag.Train = train;
            TRAIN lastTrain = null;
            try
            {
                lastTrain = db.TRAIN.Where(t => t.NAME == train.NAME && t.DETECTION_TIME < train.DETECTION_TIME).OrderByDescending(t => t.DETECTION_TIME).First();
            }
            catch (Exception ex)
            {
                //do nothing
            }
            ViewBag.LastTrain = lastTrain;
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == train.ID).OrderBy(v => v.PASS_ORDER);

            GetAEIPhotos(train);

            return View(vehicles.ToList());
        }

        private void GetAEIPhotos(TRAIN train)
        {
            string pattern = $"{train.DETECTION_TIME.ToString("yyyyMMddHHmmss")}-*";
            DirectoryInfo[] directoryInfos = new DirectoryInfo(Server.MapPath("~/AEI")).GetDirectories(pattern);
            DirectoryInfo[] videoInfos = new DirectoryInfo(Server.MapPath("~/AEIVideo")).GetDirectories(pattern);
            if (directoryInfos.Count() == 0)
            {
                pattern = $"*-{train.DETECTION_TIME.ToString("yyyyMMddHHmmss")}";
                directoryInfos = new DirectoryInfo(Server.MapPath("~/AEI")).GetDirectories(pattern);
            }
            if(videoInfos.Count() == 0)
            {
                pattern = $"*-{train.DETECTION_TIME.ToString("yyyyMMddHHmmss")}";
                videoInfos = new DirectoryInfo(Server.MapPath("~/AEIVideo")).GetDirectories(pattern);
            }
            FileInfo[] fileInfoAEI = null;
            if (directoryInfos.Count() > 0)
            {
                fileInfoAEI = directoryInfos[0].GetFiles();
            }
            if (fileInfoAEI != null && fileInfoAEI.Count() > 0)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var t in fileInfoAEI)
                {
                    //string name = t.Name.Split('.')[0];
                    //string[] nameArray = t.Name.Split('.')[0].Split('-');

                    string base64 = ImageHelper.ImgToBase64String(t.FullName);
                    string name = t.Name.Split('.')[0];
                    dict.Add(name, base64);

                }
                ViewBag.Image = dict;
            }
            FileInfo[] fileInfoVideo = null;
            if(videoInfos.Count()>0)
            {
                fileInfoVideo= videoInfos[0].GetFiles();
                string dirName = videoInfos[0].Name;
                if (fileInfoVideo.Count()>0)
                {                   
                    ViewBag.VideoName = Url.Content($"~/AEIVideo/{videoInfos[0].Name}/{fileInfoVideo[0].Name}");
                }
                
            }
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
