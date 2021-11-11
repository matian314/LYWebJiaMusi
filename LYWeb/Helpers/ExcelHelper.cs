using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LYWeb.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data.Entity;
using NPOI.SS.Util;

namespace LYWeb.Helpers
{
    public class ExcelHelper
    {
        public static void CreateStyles(IWorkbook wb, out ICellStyle style, out List<ICellStyle> styles)
        {
            style = wb.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.Alignment = HorizontalAlignment.Center;
            ICellStyle style1 = wb.CreateCellStyle();
            style1.CloneStyleFrom(style);
            //style1.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Green.Index;
            //style1.FillPattern = FillPattern.SolidForeground;
            ICellStyle style2 = wb.CreateCellStyle();
            style2.CloneStyleFrom(style);
            style2.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Blue.Index;
            style2.FillPattern = FillPattern.SolidForeground;
            ICellStyle style3 = wb.CreateCellStyle();
            style3.CloneStyleFrom(style);
            style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Red.Index;
            style3.FillPattern = FillPattern.SolidForeground;
            styles = new List<ICellStyle> { style, style1, style2, style3 };
        }

        public static void CreateStyle(IWorkbook wb, out ICellStyle style)
        {
            style = wb.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.Alignment = HorizontalAlignment.Center;
        }

        public static void CreateAlarmLevelCell(List<ICellStyle> styles, IRow rowData, int colNum, Nullable<byte> ALARM_LEVEL)
        {
            string alarmResult = "";
            ICell cell = rowData.CreateCell(colNum);
            switch (ALARM_LEVEL)
            {
                case 0:
                    alarmResult = "正常";
                    cell.CellStyle = styles[0];
                    break;
                case 1:
                    alarmResult = "正常";
                    cell.CellStyle = styles[1];
                    break;
                case 2:
                    alarmResult = "跟踪控制";
                    cell.CellStyle = styles[2];
                    break;
                case 3:
                    alarmResult = "复查判断";
                    cell.CellStyle = styles[3];
                    break;
                default:
                    alarmResult = "未知";
                    cell.CellStyle = styles[0];
                    break;
            }
            cell.SetCellValue(alarmResult);
        }

        public static void CreateCell(ICellStyle style, IRow rowData, int colNum, string content, bool isFormula = false)
        {
            ICell cell = rowData.CreateCell(colNum);
            if (isFormula)
            {
                cell.SetCellFormula(content);
            }
            else
            {
                cell.SetCellValue(content);
            }
                
            cell.CellStyle = style;
        }



        public static void CombineCellsLoop(ISheet sh, int colNum,int startRow, int combineCellsNum, int times)
        {
            for(int i=0; i< times; i++)
            {
                int fromRow = startRow;
                int destRow = startRow + combineCellsNum - 1;
                CellRangeAddress region = new CellRangeAddress(fromRow, destRow, colNum, colNum);
                sh.AddMergedRegion(region);
                startRow = destRow + 1;                
            }           
        }

        public static string TrainFaultStatus( string id)
        {
            TudsEntities db = new TudsEntities();
            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id);
            if (faults.Count() == 0)
            {
                return "无超限";
            }
            var unreckecked = faults.Where(f => f.ISRECHECKED == false);
            if (unreckecked.Count() > 0)
            {
                if (unreckecked.Where(f => f.ALARM_LEVEL == 3).Count() > 0)
                {
                    return "未处理";
                }
                else
                {
                    return "不处理";
                }
            }
            else
            {
                var onProcessed = faults.Where(f => f.IS_COMPLETED == false);
                if (onProcessed.Count() > 0)
                {
                    return "处理中";
                }
                else
                {
                    return "已处理";
                }
            }
        }

    }
}