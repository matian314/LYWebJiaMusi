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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TudsWeb.Controllers
{
    [Authorize]
    public class WheelController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: Wheel
        public ActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vehicle = db.VEHICLE.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleName = vehicle.NAME ?? "未知";
            var wheel = db.WHEEL.Where(w => w.VEHICLE_ID == id);
            return View(wheel.ToList());
        }

        // GET: Wheel/Details/5
        public ActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            if (wheel == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainName = wheel.VEHICLE.TRAIN.NAME;
            ViewBag.WheelName = wheel.NAME;
            return View(wheel);
        }
        //http://localhost:23110/Wheel/Scrape/13f341c6-da83-4b1d-885b-3944dd5176aa
        public ActionResult Scrape(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            if (wheel == null)
            {
                return HttpNotFound();
            }
            SCRAPE_DATA scrape = db.SCRAPE_DATA.Where(s => s.WHEEL_ID == id).FirstOrDefault();
            ViewBag.Wheel = wheel;
            if (scrape != null && scrape.DATA.Count() > 0)
            {
                ViewBag.WheelName = wheel.NAME;
                JArray array = (JArray)JsonConvert.DeserializeObject(scrape.DATA);
                List<double> data = array.ToObject<List<double>>();
                ViewBag.Data = scrape.DATA;
                ViewBag.Max = Math.Ceiling(data.Max());
                ViewBag.Min = Math.Floor(data.Min());
            }
            return View();
        }
        //http://localhost:23110/Wheel/Dimension/13f341c6-da83-4b1d-885b-3944dd5176aa
        public ActionResult Dimension(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            if (wheel == null)
            {
                return HttpNotFound();
            }
            DIMENSION_DATA dimension = db.DIMENSION_DATA.Where(s => s.WHEEL_ID == id).FirstOrDefault();
            ViewBag.Wheel = wheel;
            if (dimension != null)
            {
                try
                {
                    List<double[]> data = new List<double[]>();
                    foreach (var item in dimension.DATA.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            data.Add(item.PointToArray());
                        }
                    }
                    ViewBag.Data = JsonConvert.SerializeObject(data.ToArray());
                }
                catch
                {

                }
            }
            return View();
        }
        //http://localhost:23110/Wheel/Inspection/05d60613-b1bb-43f8-9ac5-4dd0b298194b
        public ActionResult Inspection(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            if (wheel == null)
            {
                return HttpNotFound();
            }
            var probes = db.PROBE_DATA.Where(p => p.TRAIN_ID == wheel.VEHICLE.TRAIN_ID).ToList();
            ViewBag.Probes = probes;
            return View(wheel);
        }

        public ActionResult Waves(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            string trainName = wheel.VEHICLE.TRAIN.NAME;
            string redirectUrl = Request.Url.ToString();
            //http://localhost:23110/Wheel/Waves/60e8b74a-8b12-4f79-b6dd-d791f73dd78a
            string axleNo, wheelLR;
            if(wheel.POSITION.StartsWith("右"))
            {
                wheelLR = "右轮";
            }
            else
            {
                wheelLR = "左轮";
            }
            axleNo = (wheel.AXLE_POSITION-1).ToString();
            redirectUrl = redirectUrl.Replace("/tuds","").Replace("/TUDS", "");
            redirectUrl = redirectUrl.Replace($"Wheel/Waves/{id}", $"waves/demo.html");
            redirectUrl = redirectUrl + $"?trainName={trainName}&AxleNo={axleNo}&wheelLR={wheelLR}&trainID={wheel.VEHICLE.TRAIN.ID}";            
            return Redirect(redirectUrl);
        }

        public ActionResult Photos(string id) 
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WHEEL wheel = db.WHEEL.Find(id);
            DateTime time = wheel.VEHICLE.TRAIN.DETECTION_TIME;
            string position = wheel.POSITION.Trim();
            ViewBag.WheelName = wheel.NAME;
            List<string> timePatterns = new List<string>();
            for (int i = 0; i <= 10; i++)
            {
                string temp2 = time.AddMinutes(-i).ToString("yyyyMMddHHmm*");
                string temp1 = time.AddMinutes(i).ToString("yyyyMMddHHmm*");
                timePatterns.Add(temp1);
                timePatterns.Add(temp2);
            }
            List<DirectoryInfo> dis = new List<DirectoryInfo>();
            foreach (var item in timePatterns)
            {
                DirectoryInfo[] directoryInfos = new DirectoryInfo(Server.MapPath("~/ScrapePhotos")).GetDirectories(item);
                if (directoryInfos.Count() != 0)
                {
                    dis.AddRange(directoryInfos);
                }
            }
            if (dis.Count() == 0)
            {
                ViewBag.Tips = "暂无数据";
                return View();
            }
            else
            {
                DirectoryInfo[] temp = dis[0].GetDirectories(WheelPositionToDirName(position));
                if (temp.Count() != 0)
                {
                    FileInfo[] fi = temp[0].GetFiles();
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (var t in fi)
                    {
                        string base64 = ImageHelper.ImgToBase64String(t.FullName);
                        string name = t.Name.Split('.')[0];
                        dict.Add(name, base64);
                    }
                    return View(dict.OrderBy(p => p.Key).ToDictionary(p => p.Key, o => o.Value));
                }
                else
                {
                    return View();
                }
            }
        }

        public string WheelPositionToDirName(string position)
        {
            position = position.Trim();
            string dir = "";
            //左6→61，右3→32，以此类推
            if (position.StartsWith("左"))
            {
                dir = position.Replace("左", "") + "1";
            }
            else if (position.StartsWith("右"))
            {
                dir = position.Replace("右", "") + "2";
            }
            return dir.Trim();
        }
        public string ImgToBase64String(string Imagefilename)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Imagefilename);
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms.Dispose();
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

    public static class Extension
    {
        public static double[] PointToArray(this string value)
        {
            double[] result = { Double.Parse(value.Split(',')[0]), Double.Parse(value.Split(',')[1]) };
            return result;
        }
    }
}
