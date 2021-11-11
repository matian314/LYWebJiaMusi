using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LYWeb.Filters;
using LYWeb.Models;
using PagedList;
namespace LYWeb.Controllers
{
    [Authorize]
    [Manager]
    public class UnitController : Controller
    {
        private TudsEntities db = new TudsEntities();

        // GET: Unit
        public ActionResult Index(string CarNumber, string CarType, int? page)
        {
            
            var uNIT_NUMBER_DICT = db.UNIT_NUMBER_DICT.Include(u => u.CAR_TYPE1).Include(u => u.TREAD_DICT);
            if (!(string.IsNullOrEmpty(CarNumber) || CarNumber.Trim()?.Length < 4))
            {
                uNIT_NUMBER_DICT = uNIT_NUMBER_DICT.Where(t => !string.IsNullOrEmpty(t.UNIT_NUMBER) && t.UNIT_NUMBER.Contains(CarNumber));
            }
            if(!string.IsNullOrEmpty(CarType))
            {
                uNIT_NUMBER_DICT = uNIT_NUMBER_DICT.Where(t => t.CAR_TYPE1.TYPE == CarType);
            }
            uNIT_NUMBER_DICT = uNIT_NUMBER_DICT.OrderBy(t => t.UNIT_NUMBER);
            ViewBag.CarNumber = CarNumber;
            ViewBag.CarType = CarType;
            var pageNumber = page ?? 1;
            var onePageUnit = uNIT_NUMBER_DICT.ToPagedList(pageNumber, 15);

            return View(onePageUnit);
        }


        // GET: Unit/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNIT_NUMBER_DICT uNIT_NUMBER_DICT = db.UNIT_NUMBER_DICT.Find(id);
            if (uNIT_NUMBER_DICT == null)
            {
                return HttpNotFound();
            }
            return View(uNIT_NUMBER_DICT);
        }

        // GET: Unit/Create
        public ActionResult Create()
        {
            ViewBag.CAR_TYPE = new SelectList(db.CAR_TYPE, "ID", "TYPE");
            ViewBag.TREAD_TYPE = new SelectList(db.TREAD_DICT, "ID", "NAME");
            return View();
        }

        // POST: Unit/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UNIT_NUMBER,DEPOT_NAME,CHECK_NAME,LAST_TIME,CAR_TYPE,TREAD_TYPE")] UNIT_NUMBER_DICT uNIT_NUMBER_DICT)
        {

            
            uNIT_NUMBER_DICT.ID = db.UNIT_NUMBER_DICT.Max(u => u.ID) + 1;

            if (TryUpdateModel(uNIT_NUMBER_DICT))
            {
                string realUnitNumber = uNIT_NUMBER_DICT.UNIT_NUMBER;
                //如果只输入数字车号，从CAR_TYPE表里查出类型加到车号前
                if (System.Text.RegularExpressions.Regex.IsMatch(uNIT_NUMBER_DICT.UNIT_NUMBER, @"[0-9]{4}"))
                {
                    CAR_TYPE cAR_TYPE = db.CAR_TYPE.Find(uNIT_NUMBER_DICT.CAR_TYPE);
                    if (cAR_TYPE != null)
                    {
                        realUnitNumber = cAR_TYPE.TYPE + "-" + uNIT_NUMBER_DICT.UNIT_NUMBER;
                    }
                }
                uNIT_NUMBER_DICT.UNIT_NUMBER = realUnitNumber;
                db.UNIT_NUMBER_DICT.Add(uNIT_NUMBER_DICT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CAR_TYPE = new SelectList(db.CAR_TYPE, "ID", "TYPE", uNIT_NUMBER_DICT.CAR_TYPE);
                ViewBag.TREAD_TYPE = new SelectList(db.TREAD_DICT, "ID", "NAME", uNIT_NUMBER_DICT.TREAD_TYPE);
                return View(uNIT_NUMBER_DICT);
            }

        }

        // GET: Unit/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNIT_NUMBER_DICT uNIT_NUMBER_DICT = db.UNIT_NUMBER_DICT.Find(id);
            if (uNIT_NUMBER_DICT == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAR_TYPE = new SelectList(db.CAR_TYPE, "ID", "TYPE", uNIT_NUMBER_DICT.CAR_TYPE);
            ViewBag.TREAD_TYPE = new SelectList(db.TREAD_DICT, "ID", "NAME", uNIT_NUMBER_DICT.TREAD_TYPE);
            return View(uNIT_NUMBER_DICT);
        }

        // POST: Unit/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UNIT_NUMBER,DEPOT_NAME,CHECK_NAME,LAST_TIME,CAR_TYPE,TREAD_TYPE")] UNIT_NUMBER_DICT uNIT_NUMBER_DICT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uNIT_NUMBER_DICT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CAR_TYPE = new SelectList(db.CAR_TYPE, "ID", "TYPE", uNIT_NUMBER_DICT.CAR_TYPE);
            ViewBag.TREAD_TYPE = new SelectList(db.TREAD_DICT, "ID", "NAME", uNIT_NUMBER_DICT.TREAD_TYPE);
            return View(uNIT_NUMBER_DICT);
        }

        // GET: Unit/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UNIT_NUMBER_DICT uNIT_NUMBER_DICT = db.UNIT_NUMBER_DICT.Find(id);
            if (uNIT_NUMBER_DICT == null)
            {
                return HttpNotFound();
            }
            return View(uNIT_NUMBER_DICT);
        }

        // POST: Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            UNIT_NUMBER_DICT uNIT_NUMBER_DICT = db.UNIT_NUMBER_DICT.Find(id);
            db.UNIT_NUMBER_DICT.Remove(uNIT_NUMBER_DICT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CarType(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                    id = db.CAR_TYPE.Where(c => c.TYPE == "NJ2").FirstOrDefault()?.ID;         
            }
            if(string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }
            CAR_TYPE type = db.CAR_TYPE.Find(id);
            ViewBag.CarType = type.ID;
            return View(type);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CarType(string submit, string CarId, CAR_TYPE car_type)
        {
            if(submit == "search")
            {
                return RedirectToAction("CarType", new { id = CarId });
            }
            else if(submit == "保存")
            {
                if(ModelState.IsValid)
                {
                    db.Entry(car_type).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("CarType", new { id = CarId });
            }
            else
            {
                return RedirectToAction("CarType", new { id = CarId });
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
