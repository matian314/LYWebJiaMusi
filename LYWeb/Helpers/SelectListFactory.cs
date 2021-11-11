using LYWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LYWeb.Helpers
{
    public static class SelectListFactory
    {
        public static SelectListItem[] CreateAlarmLevelSelectList(string viewbagValue)
        {
            SelectListItem item1 = new SelectListItem()
            {
                Value = "123",
                Text = "全部（正常、II级和III级）",
                
                Selected = IsSelected("123", viewbagValue)
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "2",
                Text = "II级",
                Selected = IsSelected("2", viewbagValue)
            };
            SelectListItem item3 = new SelectListItem()
            {
                Value = "3",
                Text = "III级",
                Selected = IsSelected("3", viewbagValue)
            };
            SelectListItem item4 = new SelectListItem()
            {
                Value = "23",
                Text = "II级和III级",
                Selected = IsSelected("23", viewbagValue)
            };
            return new SelectListItem[] { item1, item2, item3, item4 };
        }

        public static SelectListItem[] CreateCarTypeSelectList(string viewbagValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach(var item in new TudsEntities().CAR_TYPE)
            {
                result.Add(new SelectListItem()
                {
                    Value = item.ID,
                    Text = item.TYPE,
                    Selected = IsSelected(item.ID, viewbagValue)
                });
            }
            return result.ToArray();
        }

        public static bool IsSelected(string value, string viewbagValue)
        {
            if(value == viewbagValue)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }
        public static SelectListItem[] CreateRoleSelectList(WEBUSER user)
        {
            SelectListItem item1 = new SelectListItem()
            {
                Value = "管理员",
                Text = "管理员",
                Selected = false
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "值班员",
                Text = "值班员",
                Selected = false
            };
            SelectListItem item3 = new SelectListItem()
            {
                Value = "普通用户",
                Text = "普通用户",
                Selected = false
            };
            if(user.ROLE == "普通用户")
            {
                item3.Selected = true;
            }
            else if(user.ROLE == "值班员")
            {
                item2.Selected = true;
            }
            else if(user.ROLE == "管理员")
            {
                item1.Selected = true;
            }
           
            return new SelectListItem[] { item1, item2, item3 };
        }

        public static SelectListItem[] CreateUserEnabledSelectList(WEBUSER user)
        {
            SelectListItem item1 = new SelectListItem()
            {
                Value = "0",
                Text = "未激活",
                Selected = false
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "1",
                Text = "已激活",
                Selected = false
            };
            if(user.ENABLED == true)
            {
                item2.Selected = true;
            }
            else if(user.ENABLED == false)
            {
                item1.Selected = true;
            }
            return new SelectListItem[] { item1, item2};
        }
            /// <summary>
            /// 探伤，擦伤，几何尺寸等
            /// </summary>
            /// <returns></returns>
            public static SelectListItem[] CreateDataSourceSelectList(string viewbagValue)
        {
            SelectListItem item1 = new SelectListItem()
            {
                Value = "ALARM_LEVEL",
                Text = "全部",
                Selected = IsSelected("ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "DIMENSION_ALARM_LEVEL",
                Text = "几何尺寸",
                Selected = IsSelected("DIMENSION_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item3 = new SelectListItem()
            {
                Value = "SCRAPE_ALARM_LEVEL",
                Text = "擦伤",
                Selected = IsSelected("SCRAPE_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item4 = new SelectListItem()
            {
                Value = "INSPECTION_ALARM_LEVEL",
                Text = "探伤",
                Selected = IsSelected("INSPECTION_ALARM_LEVEL", viewbagValue)
            };
            return new SelectListItem[] { item1, item2, item3, item4 };
        }

        public static SelectListItem[] CreateFaultItemSelectList(string viewbagValue)
        {
            SelectListItem item1 = new SelectListItem()
            {
                Value = "ALARM_LEVEL",
                Text = "全部",
                Selected = IsSelected("ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item2 = new SelectListItem()
            {
                Value = "DIAMETER_ALARM_LEVEL",
                Text = "车轮直径",
                Selected = IsSelected("DIAMETER_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item3 = new SelectListItem()
            {
                Value = "FLANGE_THICKNESS_ALARM_LEVEL",
                Text = "轮缘厚度",
                Selected = IsSelected("FLANGE_THICKNESS_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item4 = new SelectListItem()
            {
                Value = "FLANGE_HEIGHT_ALARM_LEVEL",
                Text = "轮缘高度",
                Selected = IsSelected("FLANGE_HEIGHT_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item5 = new SelectListItem()
            {
                Value = "RIM_THICKNESS_ALARM_LEVEL",
                Text = "轮辋厚度",
                Selected = IsSelected("RIM_THICKNESS_ALARM_LEVEL", viewbagValue)
            };
            SelectListItem item6 = new SelectListItem()
            {
                Value = "TREAD_WEAR_ALARMLEVEL",
                Text = "踏面磨耗",
                Selected = IsSelected("TREAD_WEAR_ALARMLEVEL", viewbagValue)
            };
            SelectListItem item7 = new SelectListItem()
            {
                Value = "WHEELSET_DISTANCE_ALARMLEVEL",
                Text = "轮对内侧距",
                Selected = IsSelected("WHEELSET_DISTANCE_ALARMLEVEL", viewbagValue)
            };
            SelectListItem item8 = new SelectListItem()
            {
                Value = "QR_ALARMLEVEL",
                Text = "QR值",
                Selected = IsSelected("QR_ALARMLEVEL", viewbagValue)
            };
            SelectListItem item9 = new SelectListItem()
            {
                Value = "BRUISE_DEPTH_ALARMLEVEL",
                Text = "擦伤深度",
                Selected = IsSelected("BRUISE_DEPTH_ALARMLEVEL", viewbagValue)
            };
            SelectListItem item10 = new SelectListItem()
            {
                Value = "BRUISE_LENGTH_ALARMLEVEL",
                Text = "擦伤长度",
                Selected = IsSelected("BRUISE_LENGTH_ALARMLEVEL", viewbagValue)
            };
            SelectListItem item11 = new SelectListItem()
            {
                Value = "COAXIAL_DIAM_DIFF_ALARMLEVEL",
                Text = "同轴轮径差",
                Selected = IsSelected("COAXIAL_DIAM_DIFF_ALARMLEVEL", viewbagValue)
            };
            return new SelectListItem[] { item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11};
        }

        public static SelectListItem[] CreateSuggesionSelectList()
        {
            return new SelectListItem[] {new SelectListItem()
            {
                Value = "暂不处理",
                Text = "暂不处理",
            }, new SelectListItem()
            {
                Value = "跟踪控制",
                Text = "跟踪控制"
            }, new SelectListItem()
            {
                Value = "复查判断",
                Text = "复查判断"
            } };
        }

        public static string[] CreateErrorAnalysisList()
        {
            return new string[] { "误报", "正确报警" };
        }

        public static string[] CreateRecheckConclusionList()
        {
            return new string[]
            {
                "不超限",
                "跟踪观察",
                "换轮",
                "镟修",
                "扣修"
            };
        }

        public static SelectListItem[] CreateBureauSelectList()
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach(var bureau in new TudsEntities().BUREAUDICT.OrderBy(b => b.SEQUENCE_NUMBER))
            {
                var item = new SelectListItem()
                {
                    Text = bureau.NAME,
                    Value = bureau.BUREAU_ID
                };
                //if(bureau.BUREAU_ID == "B")
                //{
                //    item.Selected = true;
                //}
                result.Add(item);
            }
            return result.ToArray();
        }
    }
}