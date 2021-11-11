using LYWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYWeb.Controllers
{
    public class InspectionWaveController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: InspectionWave
        public ActionResult Index(string id)
        {

            return View();
        }
        public JsonResult SetData()
        {
            INSPECTION_DEFECT_DATA inspection_defect_data;
            using (TudsEntities db = new TudsEntities())
            {
                inspection_defect_data = db.INSPECTION_DEFECT_DATA.FirstOrDefault();
            }

            JsonResult response = new JsonResult();


            response.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            response.Data = inspection_defect_data;

            // var vehicle = db.VEHICLE.
            return response;
        }
    }
}