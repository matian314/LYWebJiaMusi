using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Models;
using Newtonsoft.Json;
using PagedList;
using Spire.Pdf.Fields;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class TrackController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: Track/Train
        public ActionResult Train(string CarNumber, string StartTime, string EndTime, int? page)
        {
            if(Request.HttpMethod == "POST")
            {
                return TrainPost(CarNumber, StartTime, EndTime, page);
            }
            if (!string.IsNullOrEmpty(CarNumber) && CarNumber.Length >= 4)
            {
                CarNumber = CarNumber.Trim();
                var train = db.TRAIN.Where(t => t.NAME.EndsWith(CarNumber));
                int i = train.Count();
                if(!string.IsNullOrEmpty(StartTime))
                {
                    DateTime start;
                    try
                    {
                        start = DateTime.Parse(StartTime);
                        
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("fail", "起始时间格式错误");
                        start = DateTime.MinValue;
                    }
                    train = train.Where(t => t.DETECTION_TIME >= start);
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    DateTime end;
                    try
                    {
                        end = DateTime.Parse(EndTime);

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("fail", "终止时间格式错误");
                        end = DateTime.MaxValue;
                    }
                    train = train.Where(t => t.DETECTION_TIME <= end);
                }
                if (train == null)
                {
                    //return HttpNotFound();
                }
                train = train.OrderByDescending(t => t.DETECTION_TIME);
                var Train = train.FirstOrDefault();
                var pageNumber = page ?? 1;
                var onePageTrain = train.ToPagedList(pageNumber, 15);
                ViewBag.CarNumber = CarNumber;
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ViewBag.Train = Train;
                return View(onePageTrain);
            }
            else if(CarNumber != null && CarNumber.Length < 4)
            {
                ViewBag.Train = null;
                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
                }
                if (string.IsNullOrEmpty(EndTime))
                {
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
                }
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ModelState.AddModelError("fail", "必须输入正确的车号信息");
                return View();
            }
            else
            {
                ViewBag.Train = null;
                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
                }
                if (string.IsNullOrEmpty(EndTime))
                {
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
                }
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                return View();
            }
        }
        [HttpPost]
        public ActionResult TrainPost(string CarNumber, string StartTime, string EndTime, int? page)
        {

            if (!string.IsNullOrEmpty(CarNumber) && CarNumber.Length >= 4)
            {
                CarNumber = CarNumber.Trim();
                var train = db.TRAIN.Where(t => t.NAME.EndsWith(CarNumber));
                int i = train.Count();
                if (!string.IsNullOrEmpty(StartTime))
                {
                    DateTime start;
                    try
                    {
                        start = DateTime.Parse(StartTime);

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("fail", "起始时间格式错误");
                        start = DateTime.MinValue;
                    }
                    train = train.Where(t => t.DETECTION_TIME >= start);
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    DateTime end;
                    try
                    {
                        end = DateTime.Parse(EndTime);

                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("fail", "终止时间格式错误");
                        end = DateTime.MaxValue;
                    }
                    train = train.Where(t => t.DETECTION_TIME <= end);
                }
                if (train == null)
                {
                    //return HttpNotFound();
                }
                train = train.OrderByDescending(t => t.DETECTION_TIME);
                var Train = train.FirstOrDefault();
                var pageNumber = page ?? 1;
                var onePageTrain = train.ToPagedList(pageNumber, 15);
                ViewBag.CarNumber = CarNumber;
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ViewBag.Train = Train;
                return View("Train",onePageTrain);
            }
            else
            {
                ViewBag.Train = null;
                if (string.IsNullOrEmpty(StartTime))
                {
                    StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
                }
                if (string.IsNullOrEmpty(EndTime))
                {
                    EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
                }
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                ModelState.AddModelError("fail", "必须输入正确的车号信息");
                return View("Train");
            }
        }
        // GET: Track/Wheel
        public ActionResult Wheel(string CarNumber, string StartTime, string EndTime, string WheelPosition,int? page)
        {
            ViewBag.VehicleSelectList = db.UNIT_NUMBER_DICT.Select(u => u.UNIT_NUMBER).ToList();
            ViewBag.WheelSelectList = new string[] { "左1","右1","左2","右2","左3","右3","左4","右4","左5","右5" ,"左6","右6" }.ToList();

            //if (string.IsNullOrEmpty(StartTime))
            //{
            //    StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
            //}
            //if (string.IsNullOrEmpty(EndTime))
            //{
            //    EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
            //}
            if (CarNumber == null || CarNumber.Length < 4 || WheelPosition == null)
            {
                ModelState.AddModelError("fail", "请输入正确的车号/轮位");
                ViewBag.StartTime = StartTime;
                ViewBag.EndTime = EndTime;
                return View();
            }
            //将DbSet类型转换为IQueryable类型
            var wheels = db.WHEEL.Select(w =>w).OrderBy(w => w.VEHICLE.TRAIN.DETECTION_TIME).Include(w => w.VEHICLE).Include(w => w.VEHICLE.TRAIN);
            if (!string.IsNullOrEmpty(CarNumber) && CarNumber.Trim().Length >= 4)
            {
                wheels = wheels.Where(w => w.VEHICLE.NAME == CarNumber);
            }
            if(string.IsNullOrEmpty(WheelPosition))
            {
                ModelState.AddModelError("fail", "请输入轮位");
            }
            wheels = wheels.Where(w => w.POSITION == WheelPosition);
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime start = DateTime.Parse(StartTime);
                wheels = wheels.Where(w => w.VEHICLE.TRAIN.DETECTION_TIME >= start);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime end = DateTime.Parse(EndTime);
                wheels = wheels.Where(w => w.VEHICLE.TRAIN.DETECTION_TIME <= end);
            }
            wheels = wheels.OrderByDescending(w => w.VEHICLE.TRAIN.DETECTION_TIME);

            var pageNumber = page ?? 1;
            var onePageWheel = wheels.ToPagedList(pageNumber, 15);

            List<WHEEL> model = wheels?.ToList();

            List<decimal?> diameter = new List<decimal?>();
            List<decimal?> flange_thickness = new List<decimal?>();
            List<decimal?> flange_height = new List<decimal?>();
            List<decimal?> rim_thickness = new List<decimal?>();
            List<decimal?> qr = new List<decimal?>();
            List<decimal?> wheelset = new List<decimal?>();
            List<decimal?> vehicle_diff = new List<decimal?>();
            List<decimal?> coaxial_diff = new List<decimal?>();
            List<decimal?> bogie_diff = new List<decimal?>();
            List<decimal?> scrape_length = new List<decimal?>();
            List<decimal?> scrape_depth = new List<decimal?>();
            List<decimal?> round = new List<decimal?>();
            foreach (WHEEL wheel in model)
            {
                diameter.Add(wheel.DIAMETER);
                flange_thickness.Add(wheel.FLANGE_THICKNESS);
                flange_height.Add(wheel.FLANGE_HEIGHT);
                rim_thickness.Add(wheel.RIM_THICKNESS);
                qr.Add(wheel.QR);
                wheelset.Add(wheel.WHEELSET_DISTANCE);
                vehicle_diff.Add(wheel.VEHICLE_DIAMETER_DIFFERENCE);
                coaxial_diff.Add(wheel.COAXIAL_DIAMETER_DIFFERENCE);
                bogie_diff.Add(wheel.BOGIE_DIAMETER_DIFFERENCE);
                scrape_depth.Add(wheel.MAX_BRUISE_DEPTH);
                scrape_length.Add(wheel.MAX_BRUISE_LENGTH);
                round.Add(wheel.ROUND);
            }
            if(model.Count != 0)
            {
                ViewBag.Wheel = model?.LastOrDefault();

                diameter.Reverse();
                flange_thickness.Reverse();
                flange_height.Reverse();
                rim_thickness.Reverse();
                qr.Reverse();
                wheelset.Reverse();
                vehicle_diff.Reverse();
                coaxial_diff.Reverse();
                bogie_diff.Reverse();
                scrape_depth.Reverse();
                scrape_length.Reverse();
                round.Reverse();

                ViewBag.Diameter = JsonConvert.SerializeObject(diameter.ToArray());
                ViewBag.FlangeThickness = JsonConvert.SerializeObject(flange_thickness.ToArray());
                ViewBag.FlangeHeight = JsonConvert.SerializeObject(flange_height.ToArray());
                ViewBag.RimThickness = JsonConvert.SerializeObject(rim_thickness.ToArray());
                ViewBag.QR = JsonConvert.SerializeObject(qr.ToArray());
                ViewBag.WheelsetDistance = JsonConvert.SerializeObject(wheelset.ToArray());
                ViewBag.VehicleDiff = JsonConvert.SerializeObject(vehicle_diff.ToArray());
                ViewBag.CoaxialDiff = JsonConvert.SerializeObject(coaxial_diff.ToArray());
                ViewBag.BogieDiff = JsonConvert.SerializeObject(bogie_diff.ToArray());
                ViewBag.ScrapeDepth = JsonConvert.SerializeObject(scrape_depth.ToArray());
                ViewBag.ScrapeLength = JsonConvert.SerializeObject(scrape_length.ToArray());
                ViewBag.Round = JsonConvert.SerializeObject(round.ToArray());
            }
            ViewBag.CarNumber = CarNumber;
            ViewBag.StartTime = StartTime;
            ViewBag.EndTime = EndTime;
            ViewBag.WheelPosition = WheelPosition;
            ViewBag.VehicleNumber = CarNumber;
            return View(onePageWheel);
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
