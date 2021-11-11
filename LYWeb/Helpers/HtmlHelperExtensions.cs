using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using LYWeb.Models;
using NPOI.OpenXmlFormats.Dml;
using System.Data.Entity;
using System.Web;
using LYWeb.Helpers;
using Newtonsoft.Json;

namespace TudsWeb.Helpers
{
    public static class HtmlHelperExtensions
    {

        public static MvcHtmlString DisplayForData<TData>(this HtmlHelper helper, TData data)
        {
            if (data == null)
                return MvcHtmlString.Create("<td class=\"danger\">未知</td>");
            else
                return MvcHtmlString.Create($"<td>{data.ToString()}</td>");
        }

        public static MvcHtmlString DisplayAlarm<T>(this HtmlHelper<T> helper, Nullable<byte> alarmLevel)
        {
            var tag = new TagBuilder("td");
            if (!alarmLevel.HasValue)
            {
                tag.SetInnerText("数据异常");
                //tag.AddCssClass("danger");
                return MvcHtmlString.Create(tag.ToString());
            }
            if (alarmLevel.Value == 1 || alarmLevel.Value == 0)
            {
                tag.SetInnerText("正常");
                return MvcHtmlString.Create(tag.ToString());
            }
            if (alarmLevel.Value == 2)
            {
                tag.AddCssClass("warning");
                tag.SetInnerText("跟踪控制");
                return MvcHtmlString.Create($"<td style=\"background-color:blue\">跟踪控制</td>");
                //return MvcHtmlString.Create(tag.ToString());
            }
            if (alarmLevel.Value == 3)
            {
                tag.SetInnerText("跟踪复核");
                tag.AddCssClass("danger");
                return MvcHtmlString.Create($"<td style=\"background-color:red\">跟踪复核</td>");
                //return MvcHtmlString.Create(tag.ToString());
            }
            return MvcHtmlString.Create(tag.ToString());
        }

        public static string TranslateAlarmLevel(Nullable<byte> alarmLevel)
        {
            if (!alarmLevel.HasValue)
            {
                return "";
            }
            if (alarmLevel.Value == 0)
            {
                return "正常";
            }
            if (alarmLevel.Value == 1)
            {
                return "正常";
            }
            if (alarmLevel.Value == 2)
            {
                return "跟踪控制";
            }
            if (alarmLevel.Value == 3)
            {
                return "复查判断";
            }
            return "无效数据";
        }

        public static MvcHtmlString DisplayAlarmLevel<T>(this HtmlHelper<T> helper, Nullable<byte> alarmLevel)
        {
            var tag = new TagBuilder("text");
            if (!alarmLevel.HasValue)
            {
                tag.SetInnerText("无效数据");
            }
            else if (alarmLevel == 0 || alarmLevel == 9)
            {
                tag.SetInnerText("正常");
            }
            else if (alarmLevel == 1)
            {
                tag.SetInnerText("正常");

            }
            else if (alarmLevel == 2)
            {
                tag.Attributes.Add("style", "color:blue");
                tag.SetInnerText("II(跟踪控制)");
            }
            else if (alarmLevel == 3)
            {
                tag.Attributes.Add("style", "color:red");
                tag.SetInnerText("III(复查判断)");
            }
            else
            {
                tag.SetInnerText("数据无效");
            }
            return MvcHtmlString.Create(tag.ToString());
        }
        /// <summary>
        /// 输入报警级别，返回特定格式的链接
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="alarmLevel"></param>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static MvcHtmlString DisplayAlarmLink(this HtmlHelper helper, Nullable<byte> alarmLevel, string actionName, string controllerName, object routeValues,bool isTargetBlank=false)
        {
            if (!alarmLevel.HasValue)
            {
                return MvcHtmlString.Empty;
            }
            object htmlAtrributes;
            string linkText;
            if(!isTargetBlank)
            switch (alarmLevel)
            {
                case 0:
                    linkText = "--";
                    htmlAtrributes = new { style = "color:black;text-decoration-line:none" };
                    break;
                case 1:
                default:
                    linkText = "正常";
                    //htmlAtrributes = new { style = "color:black;text-decoration-line:none" };
                    htmlAtrributes = new { style = "color:darkgreen" };
                    break;
                case 2:
                    linkText = "II(跟踪控制)";
                    htmlAtrributes = new { style = "color:orange" };
                    break;
                case 3:
                    linkText = "III(复查判断)";
                    htmlAtrributes = new { style = "color:red" };
                    break;
            }
            else
            {
                switch (alarmLevel)
                {
                    case 0:
                        linkText = "--"; 
                        htmlAtrributes = new { style = "color:black;text-decoration-line:none", target = "_blank" };
                        break;
                    case 1:
                    default:
                        linkText = "正常";
                        //htmlAtrributes = new { style = "color:black;text-decoration-line:none" };
                        htmlAtrributes = new { style = "color:darkgreen", target = "_blank" };
                        break;
                    case 2:
                        linkText = "II(跟踪控制)";
                        htmlAtrributes = new { style = "color:orange", target = "_blank" };
                        break;
                    case 3:
                        linkText = "III(复查判断)";
                        htmlAtrributes = new { style = "color:red", target = "_blank" };
                        break;
                }
            }


            return helper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAtrributes);
        }

        public static MvcHtmlString DisplayValueLink(this HtmlHelper helper, WHEEL wheel, string ParaName, string actionName, string controllerName, object routeValues)
        {
            decimal invalidValue = 9999.99M;
            string name = ParaName.ToUpper();
            decimal value;
            int alarmLevel;
            switch (name)
            {
                case "DIAMETER":
                    value = wheel.DIAMETER ?? invalidValue;
                    alarmLevel = wheel.DIAMETER_ALARMLEVEL ?? 0;
                    break;
                case "FLANGE_THICKNESS":
                    value = wheel.FLANGE_THICKNESS ?? invalidValue;
                    alarmLevel = wheel.FLANGE_THICKNESS_ALARMLEVEL ?? 0;
                    break;
                case "FLANGE_HEIGHT":
                    value = wheel.FLANGE_HEIGHT ?? invalidValue;
                    alarmLevel = wheel.FLANGE_HEIGHT_ALARMLEVEL ?? 0;
                    break;
                case "RIM_THICKNESS":
                    value = wheel.RIM_THICKNESS ?? invalidValue;
                    alarmLevel = wheel.RIM_THICKNESS_ALARMLEVEL ?? 0;
                    break;
                case "TREAD_WEAR":
                    value = wheel.TREAD_WEAR ?? invalidValue;
                    alarmLevel = wheel.TREAD_WEAR_ALARMLEVEL ?? 0;
                    break;
                case "WHEELSET_DISTANCE":
                    value = wheel.WHEELSET_DISTANCE ?? invalidValue;
                    alarmLevel = wheel.WHEELSET_DISTANCE_ALARMLEVEL ?? 0;
                    break;
                case "QR":
                    value = wheel.QR ?? invalidValue;
                    alarmLevel = wheel.QR_ALARMLEVEL ?? 0;
                    break;
                case "ET":
                    value = wheel.ET ?? invalidValue;
                    //ET没有报警
                    alarmLevel = 0;
                    break;
                case "MAX_BRUISE_DEPTH":
                    value = wheel.MAX_BRUISE_DEPTH ?? invalidValue;
                    alarmLevel = wheel.BRUISE_DEPTH_ALARMLEVEL ?? 0;
                    break;
                case "MAX_BRUISE_LENGTH":
                    value = wheel.MAX_BRUISE_LENGTH ?? invalidValue;
                    alarmLevel = wheel.BRUISE_LENGTH_ALARMLEVEL ?? 0;
                    break;
                case "ROUND":
                    value = wheel.ROUND ?? invalidValue;
                    //径跳没有报警
                    alarmLevel = 0;
                    break;
                case "COAXIAL_DIAMETER_DIFFERENCE":
                    //相当于只查看1位轮的报警情况，但故障可能报在其他轮上
                    //todo
                    value = wheel.COAXIAL_DIAMETER_DIFFERENCE ?? invalidValue;
                    alarmLevel = wheel.COAXIAL_DIAM_DIFF_ALARMLEVEL ?? 0;
                    break;
                case "BOGIE_DIAMETER_DIFFERENCE":
                    value = wheel.BOGIE_DIAMETER_DIFFERENCE ?? invalidValue;
                    alarmLevel = wheel.BOGIE_DIAM_DIFF_ALARMLEVEL ?? 0;
                    break;
                case "VEHICLE_DIAMETER_DIFFERENCE":
                    value = wheel.VEHICLE_DIAMETER_DIFFERENCE ?? invalidValue;
                    alarmLevel = wheel.VEHICLE_DIAM_DIFF_ALARMLEVEL ?? 0;
                    break;
                case "INSPECTION":
                    value = wheel.INSPECTION_ALARMLEVEL ?? invalidValue;
                    alarmLevel = wheel.INSPECTION_ALARMLEVEL ?? 0;
                    break;
                default:
                    throw new ArgumentException("无效的参数名称", nameof(ParaName));

            }
            object htmlAtrributes;
            switch (alarmLevel)
            {
                case 1:
                case 0:
                    htmlAtrributes = new { style = "color:black;text-decoration-line:none" };
                    break;
                case 2:
                    htmlAtrributes = new { style = "color:blue" };
                    break;
                case 3:
                default:
                    htmlAtrributes = new { style = "color:red" };
                    break;
            }
            if(value == invalidValue)
            {
                return helper.ActionLink("-", actionName, controllerName, routeValues, htmlAtrributes);
            }
            else
            {
                return helper.ActionLink(value.ToString("0.00"), actionName, controllerName, routeValues, htmlAtrributes);
            }
        }

        public static MvcHtmlString DisplayEndPosition(this HtmlHelper helper, string endPosition)
        {
            endPosition = endPosition.ToUpper();
            string result;
            if(endPosition == "2" || endPosition == "B")
            {
                result = "II端";
            }
            else if(endPosition == "1" || endPosition == "A")
            {
                result = "I端";
            }
            else
            {
                result = "-";
            }
            return MvcHtmlString.Create(result);
        }

        public static MvcHtmlString DisplayFaultLinkForTrain(this HtmlHelper helper,string id)
        {
            TudsEntities db = new TudsEntities();
            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id);
            if(faults.Count() == 0)
            {
                return MvcHtmlString.Create("无超限");
            }
            var unreckecked = faults.Where(f => f.ISRECHECKED == false);
            if(unreckecked.Count() > 0)
            {
                if(unreckecked.Where(f => f.ALARM_LEVEL == 3).Count() > 0)
                {
                    return helper.ActionLink("未处理", "Train", "Fault", new { id = id }, new { style = "color:red" });
                }
                else
                {
                    return helper.ActionLink("未处理", "Train", "Fault", new { id = id }, new { style = "color:blue" });
                }
            }
            else
            {
                var onProcessed = faults.Where(f => f.IS_COMPLETED == false);
                if(onProcessed.Count() > 0)
                {
                    return helper.ActionLink("处理中", "Train", "Fault", new { id = id }, new { style = "color:blue" });
                }
                else
                {
                    return helper.ActionLink("已处理", "Train", "Fault", new { id = id }, new { style = "color:green" });
                }
            }
        }
        public static MvcHtmlString DisplayRemark(this HtmlHelper helper, string status)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(status))
            {
                return MvcHtmlString.Create(result);
            }
            DataStatus ds = JsonConvert.DeserializeObject<DataStatus>(status);
            if(ds.AEIStatus != "T" || ds.DimensionStatus != "T" || ds.ScrapeStatus != "T" || ds.InspectionStatus != "T")
            {
                result = "缺少";
                if(ds.AEIStatus != "T")
                {
                    result += "AEI";
                }
                if (ds.DimensionStatus != "T")
                {
                    result += "几何尺寸";
                }
                if (ds.ScrapeStatus != "T")
                {
                    result += "擦伤";
                }
                if (ds.InspectionStatus != "T")
                {
                    result += "探伤";
                }
            }
            return MvcHtmlString.Create(result);
        }
        public static bool IsAlarmDealed(string id)
        {
            TudsEntities db = new TudsEntities();

            var faults = db.FAULT.Where(f => f.WHEEL.VEHICLE.TRAIN_ID == id);
            if (faults.Count() == 0)
            {
                return false;
            }
            var unreckecked = faults.Where(f => f.ISRECHECKED == false);
            if (unreckecked.Count() > 0)
            {
                if (unreckecked.Where(f => f.ALARM_LEVEL == 3).Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                IQueryable<FAULT> onProcessed = faults.Where(f => f.IS_COMPLETED == false);
                if (onProcessed.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        public static MvcHtmlString DisplayRecheckStateLink(this HtmlHelper helper, FAULT fault)
        {
            bool is_checked = fault.ISRECHECKED ?? false;
            bool is_completed = fault.IS_COMPLETED ?? false;
            //已走完全部流程
            if(is_checked && is_completed)
            {
                RECHECKS recheck = fault.RECHECKS.FirstOrDefault();
                if(recheck == null)
                {
                    throw new Exception($"没有与ID={fault.ID}对应的RECHECKS表");
                }
                if (recheck.SUGGESTION != "复查判断")
                {
                    TagBuilder tag = new TagBuilder("text");
                    tag.Attributes.Add("style", "color:green");
                    tag.InnerHtml = "已处理";
                    return MvcHtmlString.Create(tag.ToString());
                }
                if (fault.ITEM == "同轴轮径差" || fault.ITEM == "同转向架轮径差" || fault.ITEM == "同车轮径差")
                {
                    return helper.ActionLink("已处理", "Show2", "Fault", new { id = fault.ID }, new { style = "color:green" });
                }
                else
                {
                    return helper.ActionLink("已处理", "Show", "Fault", new { id = fault.ID }, new { style = "color:green" });
                }
            }
            //没开始处理
            if(!is_checked)
            {
                TagBuilder tag = new TagBuilder("text");
                if(fault.ALARM_LEVEL == 3)
                {
                    tag.Attributes.Add("style", "color:red");
                }
                else if(fault.ALARM_LEVEL == 2)
                {
                    tag.Attributes.Add("style", "color:blue");
                }

                tag.InnerHtml = "未处理";
                return MvcHtmlString.Create(tag.ToString());
            }
            if(is_checked && !is_completed)
            {
                if(fault.RECHECKS.Count != 1)
                {
                    //log here
                }
                if (fault.ITEM == "同轴轮径差" || fault.ITEM == "同转向架轮径差" || fault.ITEM == "同车轮径差")
                {
                    return helper.ActionLink("处理中", "Edit2", "Fault", new { id = fault.ID }, new { style = "color:blue" });
                }
                else
                {
                    return helper.ActionLink("处理中", "Edit", "Fault", new { id = fault.ID }, new { style = "color:blue" });
                }
            }
            {
                //is_completed == true is_checked == false， 异常状态
                TagBuilder tag = new TagBuilder("text");
                tag.Attributes.Add("style", "color:red");
                tag.InnerHtml = "状态异常";
                return MvcHtmlString.Create(tag.ToString());
            }
        }
    }
}