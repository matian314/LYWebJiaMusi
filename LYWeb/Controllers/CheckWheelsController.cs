using LYWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LYWeb.Controllers
{
    public class CheckWheelsController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: CheckWheels
        public ActionResult Index()
        {
            var Axles = db.CHECK_AXLE.OrderByDescending(t => t.PASS_TIME);
            return View(Axles);
        }

        public ActionResult Inspection(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHECK_WHEEL wheel = db.CHECK_WHEEL.Find(id);
            string trainName = "测试"+ wheel.CHECK_AXLE.PASS_TIME.ToString("yyyyMMddHHmmss");


            //WHEEL wheel = db.WHEEL.Find(id);
            //string trainName = wheel.VEHICLE.TRAIN.NAME;
            string redirectUrl = Request.Url.ToString();
            ////http://localhost:23110/CheckWheels/Inspection/75e552c7-9fcb-461e-a285-70077439d9f9
            string wheelLR;
            if (wheel.POSITION.StartsWith("右"))
            {
                wheelLR = "右轮";
            }
            else
            {
                wheelLR = "左轮";
            }
            //axleNo = (wheel.AXLE_POSITION - 1).ToString();
            redirectUrl = redirectUrl.Replace("/tuds", "").Replace("/TUDS", "");
            redirectUrl = redirectUrl.Replace($"CheckWheels/Inspection/{id}", $"waves/demo.html");
            redirectUrl = redirectUrl + $"?trainName={trainName}&AxleNo=0&wheelLR={wheelLR}&trainID={wheel.CHECK_AXLE_ID}";
            return Redirect(redirectUrl);
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