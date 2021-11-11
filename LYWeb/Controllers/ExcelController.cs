using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LYWeb.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.Entity;
using LYWeb.Helpers;
using Spire.Xls;
namespace LYWeb.Controllers
{
    [Authorize]
    public class ExcelController : Controller
    {
        private TudsEntities db = new TudsEntities();

        // GET: Excel

        public FileResult Wheel(string CarNumber, string StartTime, string EndTime, string WheelPosition, string VehicleNumber)
        {
            MemoryStream ms;
            byte[] fileContent;
            WheelCreateExcel(CarNumber, StartTime, EndTime, WheelPosition, VehicleNumber, out ms, out fileContent);
            
            if (ms != null)
            {
                ms.Dispose();
            }
            string filename = CarNumber + " "+ WheelPosition + ""   + ".xls";
            //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            return File(fileContent, "application/ms-excel", filename);

        }
        public ActionResult WheelPDF(string CarNumber, string StartTime, string EndTime, string WheelPosition, string VehicleNumber)
        {
            MemoryStream ms;
            byte[] fileContent;
            WheelCreateExcel(CarNumber, StartTime, EndTime, WheelPosition, VehicleNumber, out ms, out fileContent);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }

        private void WheelCreateExcel(string CarNumber, string StartTime, string EndTime, string WheelPosition, string VehicleNumber, out MemoryStream ms, out byte[] fileContent)
        {
            var wheels = db.WHEEL.Select(w => w).OrderBy(w => w.VEHICLE.TRAIN.DETECTION_TIME).Include(w => w.VEHICLE).Include(w => w.VEHICLE.TRAIN);
            wheels = wheels.Where(w => w.VEHICLE.TRAIN.NAME.EndsWith(CarNumber));
            wheels = wheels.Where(w => w.POSITION == WheelPosition);
            wheels = wheels.Where(w => w.VEHICLE.COACH_NUMBER.EndsWith(VehicleNumber));
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
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/Wheel.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("车号:" + wheels.First().VEHICLE.COACH_NUMBER);
            cell = sh.GetRow(2).GetCell(2);
            cell.SetCellValue("轮位:" + WheelPosition);
            cell = sh.GetRow(2).GetCell(10);
            cell.SetCellValue($"时间:{StartTime}—{EndTime}");
            int rowNum = 4;
            foreach (var item in wheels)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.VEHICLE.TRAIN.DETECTION_TIME.ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.VEHICLE.TRAIN.SITES.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.TREAD_WEAR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.FLANGE_THICKNESS?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.FLANGE_HEIGHT?.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.QR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 6, item.DIAMETER?.ToString());
                ExcelHelper.CreateCell(style, rowData, 7, item.WHEELSET_DISTANCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 8, item.COAXIAL_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 9, item.BOGIE_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 10, item.VEHICLE_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 11, item.MAX_BRUISE_DEPTH?.ToString());
                ExcelHelper.CreateCell(style, rowData, 12, item.MAX_BRUISE_LENGTH?.ToString());
                ExcelHelper.CreateCell(style, rowData, 13, item.ROUND?.ToString());
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 14, item.INSPECTION_ALARMLEVEL);

                //ExcelHelper.CreateCell(style, rowData, 11, "");//动轴轮径差
                rowNum++;
            }
            for (int i = 0; i <= 14; i++)
            {
                sh.AutoSizeColumn(i);
            }


            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }

        public FileResult FaultTrain(string id)
        {
            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id).Include(f => f.WHEEL).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);

            return GetFaultTable(faults, null, null);
        }
        public ActionResult FaultTrainPDF(string id)
        {
            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id).Include(f => f.WHEEL).OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);

            return GetFaultTablePDF(faults, null, null);
        }
        public FileResult Fault( string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem)
        {
            DateTime start;
            DateTime end;
            var faults = (IEnumerable<FAULT>)db.FAULT;
            try
            {
                start = DateTime.Parse(StartTime);
                end = DateTime.Parse(EndTime);
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME >= start);
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME <= end);
            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
            {
                //不处理
            }
            else
            {
                CarNumber = CarNumber.Trim();
                faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.NAME.EndsWith(CarNumber));
            }
            faults = faults.OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            faults = SelectListHelper.GetFaultByAlarmLevel(faults, AlarmLevel, DataSource, FaultItem);

            return GetFaultTable(faults,StartTime, EndTime );

        }

        public ActionResult FaultPDF(string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem)
        {
            DateTime start;
            DateTime end;
            var faults = (IEnumerable<FAULT>)db.FAULT;
            try
            {
                start = DateTime.Parse(StartTime);
                end = DateTime.Parse(EndTime);
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME >= start);
            faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME <= end);
            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
            {
                //不处理
            }
            else
            {
                CarNumber = CarNumber.Trim();
                faults = faults.Where(f => f.WHEEL.VEHICLE.TRAIN.NAME.EndsWith(CarNumber));
            }
            faults = faults.OrderByDescending(f => f.WHEEL.VEHICLE.TRAIN.DETECTION_TIME);
            faults = SelectListHelper.GetFaultByAlarmLevel(faults, AlarmLevel, DataSource, FaultItem);

            return GetFaultTablePDF(faults, StartTime, EndTime);

        }
        private FileResult GetFaultTable(IEnumerable<FAULT> faults,string StartTime = null, string EndTime = null )
        {
            MemoryStream ms;
            byte[] fileContent;
            GetFaultTableCreateExcel(faults, StartTime, EndTime, out ms, out fileContent);
            if (ms != null)
            {
                ms.Dispose();
            }
            string filename = "超限及处理信息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }
        private ActionResult GetFaultTablePDF(IEnumerable<FAULT> faults, string StartTime = null, string EndTime = null)
        {
            MemoryStream ms;
            byte[] fileContent;
            GetFaultTableCreateExcel(faults, StartTime, EndTime, out ms, out fileContent);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }

            return View("~/Views/Excel/RecheckNotice.cshtml");

        }

        private void GetFaultTableCreateExcel(IEnumerable<FAULT> faults, string StartTime, string EndTime, out MemoryStream ms, out byte[] fileContent)
        {
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/Fault.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ExcelHelper.CreateStyle(wb, out style);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            if (StartTime == null && EndTime == null)
            {
                cell.SetCellValue("");
            }
            else
            {
                cell.SetCellValue($"时间:{StartTime}—{EndTime}");
            }

            int rowNum = 4;
            foreach (var item in faults)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, (rowNum - 3).ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.WHEEL.VEHICLE.TRAIN.DETECTION_TIME.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.WHEEL.VEHICLE.TRAIN.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.WHEEL.POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.ITEM?.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.VALUE.ToString());
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 6, item.ALARM_LEVEL);
                ExcelHelper.CreateCell(style, rowData, 7, item.RECHECKS.FirstOrDefault()?.SUGGESTION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 8, item.RECHECKS.FirstOrDefault()?.VALUE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 9, item.RECHECKS.FirstOrDefault()?.RECHECK_PERSON?.ToString());
                ExcelHelper.CreateCell(style, rowData, 10, item.RECHECKS.FirstOrDefault()?.CONCLUSION?.ToString());

                rowNum++;
            }
            for (int i = 0; i <= 10; i++)
            {
                sh.AutoSizeColumn(i);
            }

            //ExcelHelper.CombineCellsLoop(sh, 8, 4, 2, 32);
            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }

        public FileResult PassTrain(string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem)
        {
            DateTime start;
            DateTime end;
            int i = 0;
            var trains = (IEnumerable<TRAIN>)db.TRAIN;
            try
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    start = DateTime.Parse(StartTime);
                    trains = trains.Where(t => t.DETECTION_TIME >= start);
                    i = trains.Count();
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    end = DateTime.Parse(EndTime);
                    trains = trains.Where(t => t.DETECTION_TIME <= end);
                    i = trains.Count();
                }
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
            {
                //不处理
            }
            else
            {
                CarNumber = CarNumber.Trim();
                trains = trains.Where(t => t.NAME.EndsWith(CarNumber));
            }
            trains = trains.OrderByDescending(t => t.DETECTION_TIME);
            i = trains.Count();
            trains = SelectListHelper.GetTrainByAlarmLevel(trains, AlarmLevel, DataSource, FaultItem);
            i = trains.Count();
            return GetPassTrainTable(trains);
        }

        public ActionResult PassTrainPDF(string CarNumber, string StartTime, string EndTime, string AlarmLevel, string DataSource, string FaultItem)
        {
            DateTime start;
            DateTime end;
            int i = 0;
            var trains = (IEnumerable<TRAIN>)db.TRAIN;
            try
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    start = DateTime.Parse(StartTime);
                    trains = trains.Where(t => t.DETECTION_TIME >= start);
                    i = trains.Count();
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    end = DateTime.Parse(EndTime);
                    trains = trains.Where(t => t.DETECTION_TIME <= end);
                    i = trains.Count();
                }
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            if (string.IsNullOrEmpty(CarNumber) || CarNumber.Trim().Length < 4)
            {
                //不处理
            }
            else
            {
                CarNumber = CarNumber.Trim();
                trains = trains.Where(t => t.NAME.EndsWith(CarNumber));
            }
            trains = trains.OrderByDescending(t => t.DETECTION_TIME);
            i = trains.Count();
            trains = SelectListHelper.GetTrainByAlarmLevel(trains, AlarmLevel, DataSource, FaultItem);
            i = trains.Count();
            return GetPassTrainTablePDF(trains);
        }

        public FileResult TrainHistory(string CarNumber, string StartTime, string EndTime)
        {
            var trains = db.TRAIN.Where(t => t.NAME.EndsWith(CarNumber.Trim()));
            int i = 0;
            i = trains.Count();
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime start;
                try
                {
                    start = DateTime.Parse(StartTime);

                }
                catch (Exception)
                {
                    start = DateTime.MinValue;
                }
                trains = trains.Where(t => t.DETECTION_TIME >= start);
                i = trains.Count();
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

                    end = DateTime.MaxValue;
                }
                trains = trains.Where(t => t.DETECTION_TIME <= end);
                i = trains.Count();
            }
            trains = trains.OrderByDescending(t => t.DETECTION_TIME);
            i = trains.Count();
            return GetPassTrainTable(trains);
        }

        public ActionResult TrainHistoryPDF(string CarNumber, string StartTime, string EndTime)
        {
            var trains = db.TRAIN.Where(t => !string.IsNullOrEmpty(CarNumber.Trim()) && t.NAME.EndsWith(CarNumber.Trim()));
            int i = 0;
            i = trains.Count();
            if (!string.IsNullOrEmpty(StartTime))
            {
                DateTime start;
                try
                {
                    start = DateTime.Parse(StartTime);

                }
                catch (Exception)
                {
                    start = DateTime.MinValue;
                }
                trains = trains.Where(t => t.DETECTION_TIME >= start);
                i = trains.Count();
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

                    end = DateTime.MaxValue;
                }
                trains = trains.Where(t => t.DETECTION_TIME <= end);
                i = trains.Count();
            }
            trains = trains.OrderByDescending(t => t.DETECTION_TIME);
            i = trains.Count();
            return GetPassTrainTablePDF(trains);
        }

        private FileResult GetPassTrainTable(IEnumerable<TRAIN> trains)
        {
            MemoryStream ms;
            byte[] fileContent;
            string filename;
            GetPassTrainTableCreateExcel(trains, out ms, out fileContent, out filename);
            if (ms != null)
            {
                ms.Dispose();
            }
            return File(fileContent, "application/ms-excel", filename);
        }

        private ActionResult GetPassTrainTablePDF(IEnumerable<TRAIN> trains)
        {
            MemoryStream ms;
            byte[] fileContent;
            string filename;
            GetPassTrainTableCreateExcel(trains, out ms, out fileContent, out filename);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }
            return View("~/Views/Excel/RecheckNotice.cshtml");
        }

        private void GetPassTrainTableCreateExcel(IEnumerable<TRAIN> trains, out MemoryStream ms, out byte[] fileContent, out string filename)
        {
            int ip = trains.Count();
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/PassTrain.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(1).GetCell(0);
            cell.SetCellValue("导出时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            int rowNum = 3;

            foreach (var item in trains)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, (rowNum - 2).ToString());//序号
                ExcelHelper.CreateCell(style, rowData, 1, item.DETECTION_TIME.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.END_POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.SPEED?.ToString() + "km/h");
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 5, item.DIMENSION_ALARM_LEVEL);
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 6, item.INSPECTION_ALARM_LEVEL);
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 7, item.SCRAPE_ALARM_LEVEL);
                ExcelHelper.CreateCell(style, rowData, 8, item.DEPOT?.ToString());
                ExcelHelper.CreateCell(style, rowData, 9, ExcelHelper.TrainFaultStatus(item.ID));
                rowNum++;
            }
            for (int i = 0; i <= 9; i++)
            {
                sh.AutoSizeColumn(i);
            }
            wb.Write(ms);
            fileContent = ms.ToArray();
            filename = "过车信息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            file.Dispose();
        }

        public FileResult DimensionInfo(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            string file = DimensionInfoCreateExcel(id, out ms, out fileContent);
            if (ms != null)
            {
                ms.Dispose();
            }

            string filename = file + "尺寸信息" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }
        public ActionResult DimensionInfoPDF(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            DimensionInfoCreateExcel(id, out ms, out fileContent);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }
            return View("~/Views/Excel/RecheckNotice.cshtml");
        }

        private string DimensionInfoCreateExcel(string id, out MemoryStream ms, out byte[] fileContent)
        {
            var wheel = db.WHEEL.Include(t => t.VEHICLE).Where(t => t.VEHICLE.TRAIN.ID == id).OrderBy(t => t.VEHICLE.PASS_ORDER).ThenBy(t => t.POSITION);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString("yyyy-MM-dd HH:mm:ss");
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/DimensionInfo.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            ExcelHelper.CreateStyle(wb, out style);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("车号:" + trainName);
            cell = sh.GetRow(2).GetCell(3);
            cell.SetCellValue("检测时间:" + trainTime);
            int rowNum = 4;
            foreach (var item in wheel)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.VEHICLE.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.TREAD_WEAR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.FLANGE_THICKNESS?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.FLANGE_HEIGHT?.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.QR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 6, item.DIAMETER?.ToString());
                ExcelHelper.CreateCell(style, rowData, 7, item.WHEELSET_DISTANCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 8, item.COAXIAL_DIAMETER_DIFFERENCE?.ToString());
                //ExcelHelper.CreateCell(style, rowData, 9, item.BOGIE_DIAMETER_DIFFERENCE?.ToString());
                //ExcelHelper.CreateCell(style, rowData, 10, item.VEHICLE_DIAMETER_DIFFERENCE?.ToString());
                //ExcelHelper.CreateCell(style, rowData, 11, "");//动轴轮径差
                rowNum++;
            }
            for (int i = 0; i <= 10; i++)
            {
                sh.AutoSizeColumn(i);
            }
            ExcelHelper.CombineCellsLoop(sh, 0, 4, 6*2, 1);
            //ExcelHelper.CombineCellsLoop(sh, 10, 4, 8, 8);
            //ExcelHelper.CombineCellsLoop(sh, 11, 4, 8, 8);
            //ExcelHelper.CombineCellsLoop(sh, 9, 4, 4, 16);
            //ExcelHelper.CombineCellsLoop(sh, 7, 4, 2, 32);
            //ExcelHelper.CombineCellsLoop(sh, 8, 4, 2, 6);
            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();

            return trainName + trainTime.Replace(":","");
        }

        public FileResult VehicleData(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            VehicleDataCreateExcel(id, out ms, out fileContent);


            if(ms != null)
            {
                ms.Dispose();
            }
            string filename = "列车测量数据" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }

        public ActionResult VehicleDataPDF(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            VehicleDataCreateExcel(id, out ms, out fileContent);

            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }

            return View("~/Views/Excel/RecheckNotice.cshtml");
        }

        private void VehicleDataCreateExcel(string id, out MemoryStream ms, out byte[] fileContent)
        {
            var wheel = db.WHEEL.Include(t => t.VEHICLE).Where(t => t.VEHICLE.TRAIN.ID == id).OrderBy(t => t.VEHICLE.PASS_ORDER).ThenBy(t => t.POSITION);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString();
            string siteName = trainInfo.SITES.NAME.ToString();
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/VehicleData.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("编组:" + trainName);
            cell = sh.GetRow(2).GetCell(2);
            cell.SetCellValue("探测站:" + siteName);
            cell = sh.GetRow(2).GetCell(13);
            cell.SetCellValue("检测时间:" + trainTime);
            int rowNum = 4;
            foreach (var item in wheel)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.VEHICLE.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.TREAD_WEAR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.FLANGE_THICKNESS?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.FLANGE_HEIGHT?.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.QR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 6, item.DIAMETER?.ToString());
                ExcelHelper.CreateCell(style, rowData, 7, item.WHEELSET_DISTANCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 8, item.ET?.ToString());
                ExcelHelper.CreateCell(style, rowData, 9, item.MAX_BRUISE_DEPTH?.ToString());
                ExcelHelper.CreateCell(style, rowData, 10, item.MAX_BRUISE_LENGTH?.ToString());
                ExcelHelper.CreateCell(style, rowData, 11, item.ROUND?.ToString());
                ExcelHelper.CreateAlarmLevelCell(styles, rowData, 12, item.INSPECTION_ALARMLEVEL);
                ExcelHelper.CreateCell(style, rowData, 13, item.COAXIAL_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 14, item.BOGIE_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 15, item.VEHICLE_DIAMETER_DIFFERENCE?.ToString());
                //ExcelHelper.CreateCell(style, rowData, 11, "");//动轴轮径差
                rowNum++;
            }
            for (int i = 0; i <= 15; i++)
            {
                sh.AutoSizeColumn(i);
            }
            ExcelHelper.CombineCellsLoop(sh, 0, 4, 8, 8);
            ExcelHelper.CombineCellsLoop(sh, 13, 4, 2, 32);
            ExcelHelper.CombineCellsLoop(sh, 14, 4, 4, 16);
            ExcelHelper.CombineCellsLoop(sh, 15, 4, 8, 8);


            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
            //ms.Dispose();
        }

        public FileResult Inspection(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            InspectionCreateExcel(id, out ms, out fileContent);
            string filename = "探伤" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }
        public ActionResult InspectionPDF(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            InspectionCreateExcel(id, out ms, out fileContent);

            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }
            return View("~/Views/Excel/RecheckNotice.cshtml");
        }

        private void InspectionCreateExcel(string id, out MemoryStream ms, out byte[] fileContent)
        {
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == id).Include(v => v.WHEEL).Include(v => v.TRAIN).OrderBy(v => v.PASS_ORDER);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString();
            string siteName = trainInfo.SITES.NAME.ToString();
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/Inspection.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("编组:" + trainName);
            cell = sh.GetRow(2).GetCell(1);
            cell.SetCellValue("探测站:" + siteName);
            cell = sh.GetRow(2).GetCell(3);
            cell.SetCellValue("检测时间:" + trainTime);

            int rowNum = 4;
            foreach (var vehicle in vehicles)
            {
                var wheels = vehicle.WHEEL.OrderBy(w => w.POSITION).ToList();
                for (int i = 0; i < 4; i++)
                {

                    WHEEL wheel1 = wheels[2 * i];
                    WHEEL wheel2 = wheels[2 * i + 1];
                    IRow rowData = sh.CreateRow(rowNum);
                    ExcelHelper.CreateCell(style, rowData, 0, vehicle.NAME?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 1, wheel1.POSITION?.ToString());
                    ExcelHelper.CreateAlarmLevelCell(styles, rowData, 2, wheel1.INSPECTION_ALARMLEVEL);
                    ExcelHelper.CreateCell(style, rowData, 3, wheel2.POSITION?.ToString());
                    ExcelHelper.CreateAlarmLevelCell(styles, rowData, 4, wheel2.INSPECTION_ALARMLEVEL);
                    rowNum++;
                }
            }
            //for (int i = 0; i <= 4; i++)
            //{
            //    sh.AutoSizeColumn(i);
            //}
            ExcelHelper.CombineCellsLoop(sh, 0, 4, 4, 8);


            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }

        public FileResult Scrape(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            ScrapeCreateExcel(id, out ms, out fileContent);

            if(ms != null)
            {
                ms.Dispose();
            }
            
            string filename = "擦伤" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }
        public ActionResult ScrapePDF(string id)
        {
            MemoryStream ms;
            byte[] fileContent;
            ScrapeCreateExcel(id, out ms, out fileContent);

            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }

            return View("~/Views/Excel/RecheckNotice.cshtml");
        }

        private void ScrapeCreateExcel(string id, out MemoryStream ms, out byte[] fileContent)
        {
            var vehicles = db.VEHICLE.Where(v => v.TRAIN_ID == id).Include(v => v.WHEEL).Include(v => v.TRAIN).OrderBy(v => v.PASS_ORDER);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString();
            string siteName = trainInfo.SITES.NAME.ToString();
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/Scrape.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            List<ICellStyle> styles;
            ExcelHelper.CreateStyles(wb, out style, out styles);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("编组:" + trainName);
            cell = sh.GetRow(2).GetCell(1);
            cell.SetCellValue("探测站:" + siteName);
            cell = sh.GetRow(2).GetCell(6);
            cell.SetCellValue("检测时间:" + trainTime);

            int rowNum = 4;
            foreach (var vehicle in vehicles)
            {
                var wheels = vehicle.WHEEL.OrderBy(w => w.POSITION).ToList();
                for (int i = 0; i < 4; i++)
                {

                    WHEEL wheel1 = wheels[2 * i];
                    WHEEL wheel2 = wheels[2 * i + 1];
                    IRow rowData = sh.CreateRow(rowNum);
                    ExcelHelper.CreateCell(style, rowData, 0, vehicle.NAME?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 1, wheel1.POSITION?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 2, wheel1.MAX_BRUISE_DEPTH?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 3, wheel1.MAX_BRUISE_LENGTH?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 4, wheel1.ROUND?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 5, wheel2.POSITION?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 6, wheel2.MAX_BRUISE_DEPTH?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 7, wheel2.MAX_BRUISE_LENGTH?.ToString());
                    ExcelHelper.CreateCell(style, rowData, 8, wheel2.ROUND?.ToString());

                    rowNum++;
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                sh.AutoSizeColumn(i);
            }
            ExcelHelper.CombineCellsLoop(sh, 0, 4, 4, 8);


            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }

        public FileResult DimensionAnalyse(string id)
        {
            var wheel = db.WHEEL.Include(t => t.VEHICLE).Where(t => t.VEHICLE.TRAIN.ID == id).OrderBy(t => t.VEHICLE.NAME).ThenBy(t => t.POSITION);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString();

            FileStream file = new FileStream(Server.MapPath("../../XlsTemplate/DimensionAnalyse.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            ExcelHelper.CreateStyle(wb, out style);
            MemoryStream ms = new MemoryStream();
            ICell cell = sh.GetRow(2).GetCell(0);
            cell.SetCellValue("编组:" + trainName);
            cell = sh.GetRow(2).GetCell(1);
            cell.SetCellValue("探测站:" + trainInfo.SITES.NAME);
            cell = sh.GetRow(2).GetCell(5);
            cell.SetCellValue("配属段:" + trainInfo.DEPOT);
            cell = sh.GetRow(2).GetCell(14);
            cell.SetCellValue("检测时间:" + trainInfo.DETECTION_TIME.ToString());
            int rowNum =5;
            foreach (var item in wheel)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.VEHICLE.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, null);
                ExcelHelper.CreateCell(style, rowData, 3, item.TREAD_WEAR?.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, string.Format("D{0}-C{0}", rowNum + 1),true);
                
                ExcelHelper.CreateCell(style, rowData, 5, null);
                ExcelHelper.CreateCell(style, rowData, 6, item.FLANGE_HEIGHT?.ToString());
                ExcelHelper.CreateCell(style, rowData, 7, string.Format("G{0}-F{0}", rowNum + 1), true);

                ExcelHelper.CreateCell(style, rowData, 8, null);
                ExcelHelper.CreateCell(style, rowData, 9, item.FLANGE_THICKNESS?.ToString());
                ExcelHelper.CreateCell(style, rowData, 10, string.Format("J{0}-I{0}", rowNum + 1), true);

                ExcelHelper.CreateCell(style, rowData, 11, null);
                ExcelHelper.CreateCell(style, rowData, 12, item.DIAMETER?.ToString());
                ExcelHelper.CreateCell(style, rowData, 13, string.Format("M{0}-L{0}", rowNum + 1), true);

                ExcelHelper.CreateCell(style, rowData, 14, null);
                ExcelHelper.CreateCell(style, rowData, 15, item.COAXIAL_DIAMETER_DIFFERENCE?.ToString());
                ExcelHelper.CreateCell(style, rowData, 16, string.Format("P{0}-O{0}", rowNum + 1), true);

                ExcelHelper.CreateCell(style, rowData, 17, null);

                rowNum++;
            }
            for (int i = 0; i <= 11; i++)
            {
                sh.AutoSizeColumn(i);
            }
            
            //ExcelHelper.CombineCellsLoop(sh, 8, 4, 2, 32);
            wb.Write(ms);
            Byte[] fileContent = ms.ToArray();
            file.Dispose();
            ms.Dispose();
            string filename = "尺寸准确性分析" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }


        //public FileResult OverRun(string id)
        //{
        //    var rechecks = db.FAULT.Include(t => t.RECHECKS).Include(t => t.WHEEL.VEHICLE).Where(t => t.WHEEL.VEHICLE.TRAIN.ID == id);
        //    //var rechecks = db.RECHECKS.Include(t => t.FAULT).Include(t => t.FAULT.WHEEL.VEHICLE).Where(t => t.FAULT.WHEEL.VEHICLE.TRAIN.ID == id);
        //    var trainInfo = db.TRAIN.Find(id);
        //    string trainName = trainInfo?.NAME;
        //    string trainTime = trainInfo?.DETECTION_TIME.ToString();

        //    FileStream file = new FileStream(Server.MapPath("../../XlsTemplate/Overrun.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        //    IWorkbook wb = new HSSFWorkbook(file);
        //    ISheet sh = wb.GetSheet("sheet1");
        //    ICellStyle style;
        //    List<ICellStyle> styles;
        //    ExcelHelper.CreateStyles(wb, out style, out styles);
        //    ExcelHelper.CreateStyle(wb, out style);
        //    MemoryStream ms = new MemoryStream();
        //    ICell cell = sh.GetRow(2).GetCell(0);
        //    cell.SetCellValue("编组:" + trainName);
        //    cell = sh.GetRow(2).GetCell(3);
        //    cell.SetCellValue("检测时间:" + trainTime);
        //    int rowNum = 4;
        //    foreach (var item in rechecks)
        //    {
        //        IRow rowData = sh.CreateRow(rowNum);
        //        ExcelHelper.CreateCell(style, rowData, 0, item.WHEEL.VEHICLE.NAME?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 1, item.WHEEL.POSITION?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 2, item.ITEM?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 3, item.VALUE.ToString());
        //        ExcelHelper.CreateAlarmLevelCell(styles, rowData, 4, item.ALARM_LEVEL);
        //        ExcelHelper.CreateCell(style, rowData, 5, item.RECHECKS.FirstOrDefault()?.VALUE.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 6, item.RECHECKS.FirstOrDefault()?.RECHECK_PERSON?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 7, item.RECHECKS.FirstOrDefault()?.SUGGESTION?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 8, item.RECHECKS.FirstOrDefault()?.HANDLER?.ToString());
        //        ExcelHelper.CreateCell(style, rowData, 9, item.RECHECKS.FirstOrDefault()?.REMARKS?.ToString());                
        //        rowNum++;
        //    }
        //    for (int i = 0; i <= 11; i++)
        //    {
        //        sh.AutoSizeColumn(i);
        //    }

        //    //ExcelHelper.CombineCellsLoop(sh, 8, 4, 2, 32);
        //    wb.Write(ms);
        //    Byte[] fileContent = ms.ToArray();
        //    file.Dispose();
        //    ms.Dispose();
        //    string filename = "超限及处理信息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
        //    return File(fileContent, "application/ms-excel", filename);
        //}
        public FileResult RecheckNotification(string id)
        {
            var rechecks = db.FAULT.Include(t => t.WHEEL.VEHICLE).Where(t => t.WHEEL.VEHICLE.TRAIN.ID == id);
            var trainInfo = db.TRAIN.Find(id);
            string trainName = trainInfo?.NAME;
            string trainTime = trainInfo?.DETECTION_TIME.ToString("yyyy年MM月dd日");
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            FileStream file = new FileStream(Server.MapPath("../../XlsTemplate/RecheckNotification.xlt"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            ExcelHelper.CreateStyle(wb, out style);
            MemoryStream ms = new MemoryStream();
            ICell cell = sh.GetRow(3).GetCell(0);
            string header = cell.ToString();
            header = header.Replace("Date", trainTime).Replace("CRH", trainInfo.NAME?.ToString());
            cell.SetCellValue(header);
            int rowNum = 6;
            foreach (var item in rechecks)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.WHEEL.VEHICLE.NAME?.ToString());
                ExcelHelper.CreateCell(style, rowData, 1, item.WHEEL.POSITION?.ToString());
                ExcelHelper.CreateCell(style, rowData, 2, item.ITEM?.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.VALUE.ToString());
                for(int i = 4; i<=9; i++)
                {
                    ExcelHelper.CreateCell(style, rowData, i, "");
                }               
                //ExcelHelper.CreateCell(style, rowData, 4, item.VALUE.ToString());
                //ExcelHelper.CreateCell(style, rowData, 5, item.RECHECKED_TIME.ToString());
                //ExcelHelper.CreateCell(style, rowData, 6, item.RECHECK_PERSON?.ToString());
                //ExcelHelper.CreateCell(style, rowData, 7, item.SUGGESTION?.ToString());

                //ExcelHelper.CreateCell(style, rowData, 9, item.REMARKS?.ToString());
                rowNum++;
            }
            for (int i = 0; i <= 11; i++)
            {
                sh.AutoSizeColumn(i);
            }

            //ExcelHelper.CombineCellsLoop(sh, 8, 4, 2, 32);
            wb.Write(ms);
            Byte[] fileContent = ms.ToArray();
            file.Dispose();
            ms.Dispose();
            string filename = "超限及处理信息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }

        public ActionResult RecheckNotice(string id)
        {
            var fault = db.FAULT.Include(t => t.WHEEL).Include(t => t.WHEEL.VEHICLE).Include(t => t.WHEEL.VEHICLE.TRAIN).Where(t => t.ID == id).First();
            if(fault is null)
            {
            }
            DateTime dateTime = fault.WHEEL.VEHICLE.TRAIN.DETECTION_TIME;
            string trainName = fault.WHEEL.VEHICLE.TRAIN.NAME.Trim();
            string vehName = fault.WHEEL.VEHICLE.NAME.Trim();
            string wheelPosition = fault.WHEEL.POSITION.Trim();
            string exceedItem = fault.ITEM;
            string value = fault.VALUE?.ToString();
            string templateName = null;
            if (exceedItem.EndsWith("差"))
            {
                templateName = "069-8.xlsx";
            }
            else
            {
                templateName = "069.xlsx";
            }
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(Server.MapPath("../../XlsTemplate/" + templateName) );
            Worksheet sheet = workbook.Worksheets[0];
            sheet.Range["A17"].Text = $"     {dateTime.Year}   年   {dateTime.Month}   月   {dateTime.Day}  日，轮对故障动态检测系统探伤模块/擦伤模块/尺寸模块检测到  {trainName}  列车轮对数据超限，请进行人工复核，具体内容如下表：";
            sheet.Range["A17"].Style.Font.FontName = "黑体";
            sheet.Range["A17"].Style.Font.Size = 14;

            if(templateName == "069-8.xlsx")
            {
                sheet.Range["A37"].Text = vehName;
                sheet.Range["L37"].Text = exceedItem;
                sheet.Range["Z37"].Text = wheelPosition;
                sheet.Range["AK37"].Text = value;
            }
            else
            {
                sheet.Range["A39"].Text = vehName;
                sheet.Range["L39"].Text = exceedItem;
                sheet.Range["Z39"].Text = wheelPosition;
                sheet.Range["AN39"].Text = value;
            }
            
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;

            return View();
        }
        public FileResult Stat(string CarNumber, string StartTime, string EndTime, string Site)
        {
            MemoryStream ms;
            byte[] fileContent;
            StatCreateExcel(CarNumber, StartTime, EndTime, Site, out ms, out fileContent);
            
            string filename = "统计报表" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }

        public ActionResult StatPDF(string CarNumber, string StartTime, string EndTime, string Site)
        {
            MemoryStream ms;
            byte[] fileContent;
            StatCreateExcel(CarNumber, StartTime, EndTime, Site, out ms, out fileContent);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }
            return View("~/Views/Excel/RecheckNotice.cshtml");

        }

        private void StatCreateExcel(string CarNumber, string StartTime, string EndTime, string Site, out MemoryStream ms, out byte[] fileContent)
        {
            var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            IEnumerable<StatModel> result = StatisticsHelper.SelectStatByConditions(trains, CarNumber, StartTime, EndTime, Site);
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/stat.xls"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            ExcelHelper.CreateStyle(wb, out style);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(1).GetCell(0);
            cell.SetCellValue($"统计时间:{StartTime}—{EndTime}");
            int rowNum = 3;
            foreach (var item in result)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.TRAIN_NAME);
                ExcelHelper.CreateCell(style, rowData, 1, item.SITE_NAME);
                ExcelHelper.CreateCell(style, rowData, 2, item.TRAIN_CNT.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.DIMENSION_CNT2.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.DIMENSION_CNT3.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.SCRAPE_CNT2.ToString());
                ExcelHelper.CreateCell(style, rowData, 6, item.SCRAPE_CNT3.ToString());
                ExcelHelper.CreateCell(style, rowData, 7, item.INSPECTION_CNT2.ToString());
                ExcelHelper.CreateCell(style, rowData, 8, item.INSPECTION_CNT3.ToString());
                rowNum++;
            }
            for (int i = 0; i <= 8; i++)
            {
                sh.AutoSizeColumn(i);
            }
            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }

        public FileResult StatRechecks(string FaultItem, string StartTime, string EndTime, string Site)
        {
            MemoryStream ms;
            byte[] fileContent;
            StatRechecksCreateExcel(FaultItem, StartTime, EndTime, Site, out ms, out fileContent);
            
            string filename = "复核统计报表" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".xls";
            return File(fileContent, "application/ms-excel", filename);
        }

        public ActionResult StatRechecksPDF(string FaultItem, string StartTime, string EndTime, string Site)
        {
            MemoryStream ms;
            byte[] fileContent;
            StatRechecksCreateExcel(FaultItem, StartTime, EndTime, Site, out ms, out fileContent);
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(ms);
            string relativePath = $"/Temp/{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
            string filePath = Server.MapPath(relativePath);
            workbook.SaveToFile(filePath, FileFormat.PDF);
            workbook.Dispose();
            ViewBag.pdfPath = relativePath;
            if (ms != null)
            {
                ms.Dispose();
            }

            return View("~/Views/Excel/RecheckNotice.cshtml");

        }

        private void StatRechecksCreateExcel(string FaultItem, string StartTime, string EndTime, string Site, out MemoryStream ms, out byte[] fileContent)
        {
            var faults = db.FAULT.Include(t => t.WHEEL.VEHICLE.TRAIN.SITES);
            //var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            IEnumerable<StatReckecksModel> result = StatisticsHelper.SelectStatRecheckByConditions(faults, FaultItem, StartTime, EndTime, Site);
            FileStream file = new FileStream(Server.MapPath("~/XlsTemplate/StatRechecks.xls"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            IWorkbook wb = new HSSFWorkbook(file);
            ISheet sh = wb.GetSheet("sheet1");
            ICellStyle style;
            ExcelHelper.CreateStyle(wb, out style);
            ms = new MemoryStream();
            ICell cell = sh.GetRow(1).GetCell(0);
            cell.SetCellValue($"统计时间:{StartTime}—{EndTime}");
            int rowNum = 3;
            foreach (var item in result)
            {
                IRow rowData = sh.CreateRow(rowNum);
                ExcelHelper.CreateCell(style, rowData, 0, item.SITE_NAME);
                ExcelHelper.CreateCell(style, rowData, 1, item.FAULT_ITEM);
                ExcelHelper.CreateCell(style, rowData, 2, item.NEED_RECHECK_CNT.ToString());
                ExcelHelper.CreateCell(style, rowData, 3, item.RECHECKED_CNT.ToString());
                ExcelHelper.CreateCell(style, rowData, 4, item.CORRECT_ALARM_CNT.ToString());
                ExcelHelper.CreateCell(style, rowData, 5, item.FALSE_ALARM_CNT.ToString());

                rowNum++;
            }
            for (int i = 0; i <= 5; i++)
            {
                sh.AutoSizeColumn(i);
            }
            wb.Write(ms);
            fileContent = ms.ToArray();
            file.Dispose();
        }
    }
}