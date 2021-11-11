using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LYWeb.Helpers
{
    public class LocoReportHelper
    {
        public static string GetAlarmLevelStrByNumber(byte? level, bool isOnlyReturnFault = false)
        {
            string levelStr = null;
            if (level.HasValue==false)
            {
                if(isOnlyReturnFault)
                {
                    return "无数据";
                }
                else
                { 
                    return levelStr;
                }
            }
            switch ((int)level)
            {
                case 1:
                    if (!isOnlyReturnFault)
                    {
                        levelStr = "正常";
                    }                    
                    break;
                case 2:
                    if (isOnlyReturnFault)
                    {
                        levelStr = "[II级]";
                    }
                    else
                    {
                        levelStr = "II级";
                    }                   
                    break;
                case 3:
                    if (isOnlyReturnFault)
                    {
                        levelStr = "[III级]";
                    }
                    else
                    {
                        levelStr = "III级";
                    }
                    break;
                default:
                    if (!isOnlyReturnFault)
                    {
                        levelStr = "无数据";
                    }
                    
                    break;
            }
            return levelStr;
        }


        public static void ReplaceWithString(Document doc, string temp, string value)
        {
            TextSelection selection = doc.FindString(temp, true, false);
            TextRange range = selection.GetAsOneRange();
            range.Text = value;
        }

        public static void InsertTable(Document doc, Section section, string replaceString, DataTable dt)
        {
            TextSelection selection = doc.FindString(replaceString, true, true);

            //获取关键字符串所在段落的索引
            TextRange range = selection.GetAsOneRange();
            Paragraph paragraph = range.OwnerParagraph;
            Body body = paragraph.OwnerTextBody;
            int index = body.ChildObjects.IndexOf(paragraph);

            //添加一个两行三列的表格
            Table table;
            CreateTable("", section,dt, ref range, out table);

            //移除段落，插入表格 
            //body.ChildObjects.Remove(paragraph);
            body.ChildObjects.Insert(index + 1, table);




        }

        public static void CreateTable(string tableType, Section section,DataTable dt, ref TextRange range, out Table table)
        {
            table = section.AddTable(true);
            table.ResetCells(dt.Rows.Count+1, dt.Columns.Count);
            for(int i=0; i<dt.Columns.Count; ++i)
            {
                range = table[0, i].AddParagraph().AppendText(dt.Columns[i].ColumnName);
                table[0, i].CellFormat.BackColor = System.Drawing.Color.LightBlue;
            }
            for(int i=1;i<dt.Rows.Count + 1; ++i)
            {
                for(int j=0; j<dt.Columns.Count;++j)
                {
                    range = table[i, j].AddParagraph().AppendText(dt.Rows[i-1][j].ToString());
                }
            }


            table.FirstRow.IsHeader = true;
            table.AutoFit(AutoFitBehaviorType.AutoFitToWindow);

        }
    }
}