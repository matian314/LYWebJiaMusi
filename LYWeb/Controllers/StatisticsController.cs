using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Models;
using LYWeb.Helpers;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {

        private TudsEntities db = new TudsEntities();
        
        public ActionResult Index()
        {
            ViewBag.CarNumber = "";
            ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
            ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
            ViewBag.Site = "";
            return View();
        }
        public ActionResult Rechecks()
        {
            ViewBag.FaultItem = "";
            ViewBag.StartTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd") + " 18:00:00";
            ViewBag.EndTime = DateTime.Now.ToString("yyyy-MM-dd") + " 18:00:00";
            ViewBag.Site = "";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string CarNumber, string StartTime, string EndTime, string Site,string status, string year,string season ,string month,string day)
        {
            GetTimeRange(ref StartTime, ref EndTime, status, year, season, month, day);

            var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            IEnumerable<StatModel> result = StatisticsHelper.SelectStatByConditions(trains, CarNumber, StartTime, EndTime, Site);
            ViewBag.CarNumber = CarNumber;
            ViewBag.StartTime = StartTime;
            ViewBag.EndTime = EndTime;
            ViewBag.Site = Site;
            return View(result.ToList());
        }

        private static void GetTimeRange(ref string StartTime, ref string EndTime, string status, string year, string season, string month, string day)
        {
            int iYear, iSeason, iMonth, iDay;
            switch (status)
            {
                case "0":
                    break;
                case "byYear":
                    if (int.TryParse(year, out iYear))
                    {
                        StartTime = (new DateTime(iYear, 1, 1)).ToString("yyyy-MM-dd") + " 18:00:00";
                        EndTime = (new DateTime(iYear, 1, 1).AddYears(1)).ToString("yyyy-MM-dd") + " 18:00:00";
                    }
                    break;
                case "byMonth":
                    if (int.TryParse(year, out iYear) && int.TryParse(month, out iMonth))
                    {
                        StartTime = (new DateTime(iYear, iMonth, 1)).ToString("yyyy-MM-dd") + " 18:00:00";
                        EndTime = (new DateTime(iYear, iMonth, 1).AddMonths(1)).ToString("yyyy-MM-dd") + " 18:00:00";
                    }
                    break;
                case "bySeason":
                    if (int.TryParse(year, out iYear) && int.TryParse(season, out iSeason))
                    {
                        int[] arr = { 0, 1, 4, 7, 10 };
                        StartTime = (new DateTime(iYear, arr[iSeason], 1)).ToString("yyyy-MM-dd") + " 18:00:00";
                        EndTime = (new DateTime(iYear, arr[iSeason], 1).AddMonths(3).AddDays(-1)).ToString("yyyy-MM-dd") + " 18:00:00";
                    }
                    break;
                case "byDay":
                    if (int.TryParse(year, out iYear) && int.TryParse(month, out iMonth) && int.TryParse(day, out iDay))
                    {
                        StartTime = (new DateTime(iYear, iMonth, iDay)).ToString("yyyy-MM-dd") + " 18:00:00";
                        EndTime = (new DateTime(iYear, iMonth, iDay).AddDays(1)).ToString("yyyy-MM-dd") + " 18:00:00";
                    }
                    break;
                default:
                    break;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rechecks(string FaultItem, string StartTime, string EndTime, string Site, string status, string year, string season, string month, string day)
        {
            GetTimeRange(ref StartTime, ref EndTime, status, year, season, month, day);
            var faults = db.FAULT.Include(t => t.WHEEL.VEHICLE.TRAIN.SITES);
            var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            IEnumerable<StatReckecksModel> result = StatisticsHelper.SelectStatRecheckByConditions(faults, FaultItem, StartTime, EndTime, Site);
            ViewBag.FaultItem = FaultItem;
            ViewBag.StartTime = StartTime;
            ViewBag.EndTime = EndTime;
            ViewBag.Site = Site;
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
