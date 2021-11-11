using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Models;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class Device_StatusController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: Device_Status
        public ActionResult Index()
        {
            return View(db.DEVICE_STATUS.ToList());
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
