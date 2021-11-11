using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using LYWeb.Helpers;
using Spire.Xls;
using System.Linq;
using System;
using Spire.Doc;
using System.Collections.Generic;
using System.Data;
using LYWeb.Models;

namespace LYWeb.Controllers
{
    [Authorize]
    public class LocoReportController : Controller
    {
        private TudsEntities db = new TudsEntities();
        //\Wheel\Dimension
        public ActionResult WheelDimension(string id)
        {

            var wheel = db.WHEEL.Include(t => t.VEHICLE.TRAIN).Where(t => t.ID == id).First();
            //StatRechecksCreateExcel(FaultItem, StartTime, EndTime, Site, out ms, out fileContent);
            string templateName = "WheelDimension.xlsx";
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("../../XlsTemplate/" + templateName));
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Range["B5"].Text = wheel.VEHICLE.TRAIN.DETECTION_TIME.ToString("yyyy-MM-dd HH:mm:ss");
            sheet.Range["E5"].Text = wheel.VEHICLE.TRAIN.NAME;
            sheet.Range["E6"].Text = wheel.VEHICLE.TRAIN.NAME;
            sheet.Range["B7"].Text = wheel.AXLE_POSITION.ToString() + "轴";
            sheet.Range["E7"].Text = wheel.POSITION.ToString();

            sheet.Range["B13"].Text = wheel.DIAMETER.ToString();
            sheet.Range["C13"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.DIAMETER_ALARMLEVEL);
            sheet.Range["B14"].Text = wheel.TREAD_WEAR.ToString();
            sheet.Range["C14"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.TREAD_WEAR_ALARMLEVEL);
            sheet.Range["B15"].Text = wheel.FLANGE_THICKNESS.ToString();
            sheet.Range["C15"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.FLANGE_THICKNESS_ALARMLEVEL);
            sheet.Range["B16"].Text = wheel.RIM_THICKNESS.ToString();
            sheet.Range["C16"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.RIM_THICKNESS_ALARMLEVEL);
            sheet.Range["B17"].Text = wheel.QR.ToString();
            sheet.Range["C17"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.QR_ALARMLEVEL);
            sheet.Range["B18"].Text = wheel.WHEELSET_DISTANCE.ToString();
            sheet.Range["C18"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(wheel.WHEELSET_DISTANCE_ALARMLEVEL);

            string relativePath = $"/Temp/单轮外形尺寸报告{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, Spire.Xls.FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }
        //vehicle\data
        public ActionResult SysStatus(string id)
        {
            var train = db.TRAIN.Find(id);
            if(train is null)
            {
                return HttpNotFound();
            }
            
            string templateName = "sysStatus.xlsx";
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("../../XlsTemplate/" + templateName));
            Worksheet sheet = workbook.Worksheets[0];

            sheet.Range["C4"].Text = train.DETECTION_TIME.ToString("yyyy-MM-dd HH:mm:ss");
            sheet.Range["G4"].Text = train.NAME;
            sheet.Range["G5"].Text = train.AXLE_COUNT.ToString()+"轴";
            sheet.Range["D10"].Text = (2*(int)train.AXLE_COUNT).ToString();
            sheet.Range["F10"].Text = (2 * (int)train.AXLE_COUNT).ToString();

            sheet.Range["B11"].Text = train.SPEED.ToString();
            sheet.Range["H10"].Text = train.SPEED.ToString();


            string relativePath = $"/Temp/系统状态报告{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, Spire.Xls.FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }
        //vehicle\data
        public ActionResult DetectionReport(string id)
        {
            var train = db.TRAIN.Find(id);
            var wheels = db.WHEEL.Where(t => t.VEHICLE.TRAIN.ID == id).OrderBy(t => t.AXLE_POSITION).OrderByDescending(t => t.POSITION);
            if (train is null ||wheels.Count() == 0)
            {
                return HttpNotFound();
            }

            string templateName = "detectionReport.xlsx";
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("../../XlsTemplate/" + templateName));
            Worksheet sheet = workbook.Worksheets[0];



            sheet.Range["B4"].Text = train.DETECTION_TIME.ToString("yyyy-MM-dd HH:mm:ss");
            sheet.Range["F4"].Text = train.NAME;
            sheet.Range["K4"].Text = train.AXLE_COUNT.ToString() + "轴";

            //机车6轴12个轮，不应支持动车8*8=64
            if (wheels.Count() == 12 || wheels.Count() == 8*8)
            {
                int currLineNum = 9;
                foreach(var v in wheels)
                {
                    sheet.Range[$"A{currLineNum}"].Text = "1车";
                    sheet.Range[$"B{currLineNum}"].Text = train.NAME;
                    sheet.Range[$"C{currLineNum}"].Text = train.END_POSITION;
                    sheet.Range[$"D{currLineNum}"].Text = v.POSITION;
                    sheet.Range[$"E{currLineNum}"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(v.INSPECTION_ALARMLEVEL);
                    sheet.Range[$"F{currLineNum}"].Text = LocoReportHelper.GetAlarmLevelStrByNumber(v.SCRAPE_ALARMLEVEL);
                    sheet.Range[$"G{currLineNum}"].Text = v.DIAMETER.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.DIAMETER_ALARMLEVEL, true);
                    sheet.Range[$"H{currLineNum}"].Text = v.TREAD_WEAR.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.TREAD_WEAR_ALARMLEVEL, true);
                    sheet.Range[$"I{currLineNum}"].Text = v.FLANGE_THICKNESS_ALARMLEVEL.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.FLANGE_THICKNESS_ALARMLEVEL, true);
                    sheet.Range[$"J{currLineNum}"].Text = v.RIM_THICKNESS.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.RIM_THICKNESS_ALARMLEVEL, true);
                    sheet.Range[$"K{currLineNum}"].Text = v.QR.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.QR_ALARMLEVEL, true);
                    sheet.Range[$"L{currLineNum}"].Text = v.WHEELSET_DISTANCE.ToString() + LocoReportHelper.GetAlarmLevelStrByNumber(v.WHEELSET_DISTANCE_ALARMLEVEL, true);
                    currLineNum++;
                }
            }
            sheet.Range["A9:A20"].Merge();
            sheet.Range["B9:B20"].Merge();
            sheet.Range["C9:C20"].Merge();
            for(int i=9; i<20;i+=2)
            {
                int j = i + 1;
                sheet.Range[$"L{i}:L{j}"].Merge();
            }
            sheet.Range["A9:L20"].Style.VerticalAlignment = VerticalAlignType.Center;
            sheet.Range["A9:L20"].Style.HorizontalAlignment = HorizontalAlignType.Center;


            string relativePath = $"/Temp/检测报告{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, Spire.Xls.FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }
        //vehicle\data
        public ActionResult FaultReport(string id)
        {

            var train = db.TRAIN.Find(id);
            var fault = db.FAULT.Where(t => t.WHEEL.VEHICLE.TRAIN.ID == id);
            var faultDimension = fault.Where(t => t.FAULT_TYPE == "几何尺寸");
            var faultScrape = fault.Where(t => t.FAULT_TYPE == "擦伤");
            var faultInspection = fault.Where(t => t.FAULT_TYPE == "探伤");            
            string templateName = "FaultReport.docx";
            Document doc = new Document();
            doc.LoadFromFile(Server.MapPath("../../XlsTemplate/" + templateName));
            Section section = doc.Sections[0];
            LocoReportHelper.ReplaceWithString(doc, "#TIME#", train.DETECTION_TIME.ToString("yyyy-MM-dd HH:mm:ss"));
            LocoReportHelper.ReplaceWithString(doc, "#NAME#", train.NAME);
            LocoReportHelper.ReplaceWithString(doc, "#AXLE#", train.AXLE_COUNT.ToString());

            if (faultInspection.Count() != 0)
            {
                DataTable dt = new DataTable();

                foreach (var v in faultInspection)
                {
                    dt.Rows.Add("1车", v.WHEEL.VEHICLE.NAME,v.WHEEL.POSITION, LocoReportHelper.GetAlarmLevelStrByNumber(v.ALARM_LEVEL));
                }
                dt.Columns.Add("车号", typeof(string));
                dt.Columns.Add("机车号", typeof(string));
                dt.Columns.Add("轮位", typeof(string));
                dt.Columns.Add("缺陷报警", typeof(string));
                LocoReportHelper.InsertTable(doc, section, "没有发现有探伤II级报警以上缺陷报警车轮", dt);
                int count2 = faultInspection.Where(t => t.ALARM_LEVEL == 2).Count();
                int count3 = faultInspection.Where(t => t.ALARM_LEVEL == 3).Count();
                //
                LocoReportHelper.ReplaceWithString(doc, "没有发现有探伤II级报警以上缺陷报警车轮", $"发现有探伤II级报警以上缺陷报警车轮{faultInspection.Count()}个，共有III级报警{count3}处，II级报警{count2}处");
            }
            if(faultScrape.Count()!=0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("车号", typeof(string));
                dt.Columns.Add("机车号", typeof(string));
                dt.Columns.Add("轮位", typeof(string));
                dt.Columns.Add("缺陷报警", typeof(string));
                dt.Columns.Add("擦伤深度", typeof(string));
                foreach (var v in faultScrape)
                {
                    dt.Rows.Add("1车", v.WHEEL.VEHICLE.NAME, v.WHEEL.POSITION, LocoReportHelper.GetAlarmLevelStrByNumber(v.ALARM_LEVEL), v.ITEM == "擦伤深度" ? v.VALUE.ToString() : "");
                }
                //dt.Columns[0].ColumnName = "车号";
                //dt.Columns[1].ColumnName = "机车号";
                //dt.Columns[2].ColumnName = "轮位";
                //dt.Columns[3].ColumnName = "缺陷报警";
                //dt.Columns[4].ColumnName = "擦伤深度";
                LocoReportHelper.InsertTable(doc, section, "没有发现有擦伤II级报警以上缺陷报警车轮", dt);
                LocoReportHelper.ReplaceWithString(doc, "没有发现有擦伤II级报警以上缺陷报警车轮", $"发现有II级报警以上缺陷报警车轮{faultScrape.Count()}处");
            }
            if(faultDimension.Count()!=0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("车号", typeof(string));
                dt.Columns.Add("机车号", typeof(string));
                dt.Columns.Add("轮位", typeof(string));
                dt.Columns.Add("轮径", typeof(string));
                dt.Columns.Add("踏面磨耗", typeof(string));
                dt.Columns.Add("轮缘厚度", typeof(string));
                dt.Columns.Add("轮辋厚度", typeof(string));
                dt.Columns.Add("QR值", typeof(string));
                dt.Columns.Add("内侧距", typeof(string));
                HashSet<string> set = new HashSet<string>();
                foreach ( var v in faultDimension)
                {
                    set.Add(v.WHEEL_ID);
                }
                var wheels = db.WHEEL.Where(t => set.Contains(t.ID)).OrderBy(t=>t.POSITION);
                foreach( var v in wheels)
                {
                    dt.Rows.Add("1车", v.VEHICLE.NAME, v.POSITION, LocoReportHelper.GetAlarmLevelStrByNumber(v.DIAMETER_ALARMLEVEL,true), LocoReportHelper.GetAlarmLevelStrByNumber(v.TREAD_WEAR_ALARMLEVEL, true), LocoReportHelper.GetAlarmLevelStrByNumber(v.FLANGE_THICKNESS_ALARMLEVEL, true), LocoReportHelper.GetAlarmLevelStrByNumber(v.RIM_THICKNESS_ALARMLEVEL, true), LocoReportHelper.GetAlarmLevelStrByNumber(v.QR_ALARMLEVEL, true), LocoReportHelper.GetAlarmLevelStrByNumber(v.WHEELSET_DISTANCE_ALARMLEVEL, true));
                }
                LocoReportHelper.InsertTable(doc, section, "没有发现轮缘厚度预警或超限车轮", dt);
                LocoReportHelper.ReplaceWithString(doc, "没有发现轮缘厚度预警或超限车轮", $"发现有发现轮缘厚度预警或超限车轮{set.Count()}个");
            }



            string relativePath = $"/Temp/单轮外形尺寸报告{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            doc.SaveToFile(filePath, Spire.Doc.FileFormat.PDF);
            doc.Dispose();
            ViewBag.pdfPath = relativePath;
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }
        public ActionResult DailyReport()
        {
            ViewBag.Today = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.Tips = null;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult DailyReport(string date)
        {
            MemoryStream ms = new MemoryStream();
            string[] dateArray = date.Split(' ')[0].Split('-');
            DateTime dateTime;
            try
            {
                dateTime = new DateTime(int.Parse(dateArray[0]), int.Parse(dateArray[1]), int.Parse(dateArray[2]));
            }
            catch(Exception ex)
            {
                ViewBag.Today = date;
                ViewBag.Tips = "日期有错误";
                return View();
            }
            DateTime start = dateTime.AddHours(6);
            DateTime end = start.AddDays(1);

            var trains = db.TRAIN.Where(t => (t.DETECTION_TIME >= start && t.DETECTION_TIME <= end && (t.DIMENSION_ALARM_LEVEL==2 || t.DIMENSION_ALARM_LEVEL ==3 || t.SCRAPE_ALARM_LEVEL ==2 ||t.SCRAPE_ALARM_LEVEL ==3 || t.INSPECTION_ALARM_LEVEL==2 || t.INSPECTION_ALARM_LEVEL==3 )));

            if(trains.Count() == 0)
            {
                ViewBag.Today = date;
                ViewBag.Tips = "当日无报警";
                return View();
            }
            string templateName = "dailyReport.xlsx";
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("~/XlsTemplate/" + templateName));
            Worksheet sheet = workbook.Worksheets[0];
            int currLineNum = 6;
            foreach (var v in trains)
            {
                sheet.Range[$"A{currLineNum}"].Text = (currLineNum-5).ToString();
                sheet.Range[$"C{currLineNum}"].Text = v.NAME;
                ++currLineNum;
            }
            sheet.Range["B2"].Text = trains.Count().ToString();
            if(currLineNum < 16)
            {
                currLineNum = 16;
            }
            sheet.Range[$"A{currLineNum}:J{currLineNum}"].Merge();
            sheet.Range[$"A{currLineNum}"].Text = "上报时间每日06:00-次日06:00";
            workbook.SaveToStream(ms);
            byte[] fileContent = ms.ToArray();
            if (ms != null)
            {
                ms.Dispose();
            }
            string filename = "轮对数据分析日报" + date.Split(' ')[0] + ".xlsx";
            return File(fileContent, "application/ms-excel", filename);
        }


    }
}