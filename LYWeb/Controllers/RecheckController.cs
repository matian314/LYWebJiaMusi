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
    public class RecheckController : Controller
    {

        private TudsEntities db = new TudsEntities();
        // GET: Recheck/Create
        public ActionResult Create(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View();
        }

        // POST: Recheck/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HANDLER,SUGGESTION,CONCLUSION")] RECHECKS recheck, string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fault = db.FAULT.Find(id);
            if(fault == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            recheck.FAULT_ID = id;
            recheck.ID = Guid.NewGuid().ToString();
            recheck.DEALT_TIME = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.RECHECKS.Add(recheck);
                fault.ISRECHECKED = true;
                db.Entry(fault).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Fault", null);
            }
            return View(recheck);
        }

        // GET: Recheck/Index
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var recheck = db.RECHECKS.Where(r => r.FAULT_ID == id).FirstOrDefault();
            if(recheck == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(recheck);
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
