using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LYWeb.Models.MetaData
{
    public class AlarmTrain
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Time { get; set; }

        public bool IsAlarmed { get; set; }



        public AlarmTrain(TRAIN train)
        {
            ID = train.ID;
            Name = train.NAME;
            Time = train.DETECTION_TIME.ToString("yyyy/MM/dd HH:mm:ss");
            IsAlarmed = false;
        }

        //public bool IsInList(List<AlarmTrain> list)
        //{
        //    foreach(AlarmTrain at in list)
        //    {
        //        if(ID == at.ID)
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //public static string GetAlarmString(ref List<AlarmTrain> list)
        //{
        //    if(list == null)
        //    {
        //        return "";
        //    }
        //    StringBuilder sb = new StringBuilder("");
        //    foreach (AlarmTrain alarmTrain in list)
        //    {
        //        if (alarmTrain.IsAlarmed == false)
        //        {
        //            sb.Append($"\r\n{alarmTrain.Time} {alarmTrain.Name}");
        //            alarmTrain.IsAlarmed = true;
        //        }
        //    }
        //    return sb.ToString();

        //}

        //public static void GetSession(HttpApplication application, TRAIN train)
        //{
        //    if(TudsWeb.Helpers.HtmlHelperExtensions.IsAlarmDealed(null,train.ID))
        //    {

        //    }
        //    application.Session[""]
        //}
    }
}