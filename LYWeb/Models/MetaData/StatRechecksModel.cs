using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LYWeb.Models
{
    
    public class StatReckecksModel
    {
        private static TudsEntities db;
        [DisplayName("探测点")]
        public string SITE_NAME { get; set; }
        [DisplayName("故障类型")]
        public string FAULT_ITEM { get; set; }
        [DisplayName("需复核数量")]
        public Nullable<int> NEED_RECHECK_CNT { get; set; }
        [DisplayName("已复核数量")]
        public Nullable<int> RECHECKED_CNT { get; set; }
        [DisplayName("有故障数量")]
        public Nullable<int> CORRECT_ALARM_CNT { get; set; }
        [DisplayName("误报数量")]
        public Nullable<int> FALSE_ALARM_CNT { get; set; }

        internal static List<StatReckecksModel> CreateStatModelList(IQueryable<FAULT> faults)
        {
            List<StatReckecksModel> models = new List<StatReckecksModel>();
            List<string> siteNames = faults.Select(t => t.WHEEL.VEHICLE.TRAIN.SITES.NAME).Distinct().ToList();
            List<string> faultItems = faults.Select(t => t.ITEM).Distinct().ToList();
            foreach (string siteName in siteNames)
            {
                foreach (string faultItem in faultItems)
                {
                    IQueryable<FAULT> f = faults.Where(t => t.WHEEL.VEHICLE.TRAIN.SITES.NAME == siteName).Where(t => t.ITEM == faultItem);
                    if (f.Count() == 0)
                    {
                        continue;
                    }
                    StatReckecksModel srm = new StatReckecksModel();
                    srm.SITE_NAME = siteName;
                    srm.FAULT_ITEM = faultItem;

                    int correctAlarm = 0;
                    int falseAlarm = 0;
                    int rechecksCount = 0;
                    db = new TudsEntities();
                    List<FAULT> fItems = f.ToList();
                    foreach (FAULT item in fItems)
                    {

                        if (0 == db.RECHECKS.Where(t => t.FAULT_ID == item.ID).Count())
                        {
                            continue;
                        }
                        rechecksCount++;
                        RECHECKS r = db.RECHECKS.Where(t => t.FAULT_ID == item.ID).First();
                        string errorAnalysis = r.ERROR_ANALYSIS;
                        if (errorAnalysis == "误报")
                        {
                            falseAlarm++;
                        }
                        else if (errorAnalysis == "正确报警")
                        {
                            correctAlarm++;
                        }
                    }
                    srm.CORRECT_ALARM_CNT = correctAlarm;
                    srm.FALSE_ALARM_CNT = falseAlarm;
                    srm.NEED_RECHECK_CNT = rechecksCount;
                    srm.RECHECKED_CNT = correctAlarm + falseAlarm;
                    models.Add(srm);
                }
            }
            return models;
        }

        public static StatReckecksModel CalculateTotal(List<StatReckecksModel> models)
        {
            int totalNeedRecheck = 0;
            int totalRechecked = 0;
            int totalCorrectAlarm = 0;
            int totalFalseAlarm = 0;
            foreach (StatReckecksModel srm in models)
            {
                totalNeedRecheck += srm.NEED_RECHECK_CNT ?? 0;
                totalRechecked += srm.RECHECKED_CNT ?? 0;
                totalCorrectAlarm += srm.CORRECT_ALARM_CNT ?? 0;
                totalFalseAlarm += srm.FALSE_ALARM_CNT ?? 0;
            }
            StatReckecksModel total = new StatReckecksModel();
            total.SITE_NAME = "总计";
            total.FAULT_ITEM = "——";
            total.NEED_RECHECK_CNT = totalNeedRecheck;
            total.RECHECKED_CNT = totalRechecked;
            total.CORRECT_ALARM_CNT = totalCorrectAlarm;
            total.FALSE_ALARM_CNT = totalFalseAlarm;
            return total;
        }






        //public static IEnumerable<StatModel> CreateStatReckecksList(IQueryable<FAULT> faults)
        //{
        //    List<StatReckecksModel> models = new List<StatReckecksModel>();
        //    faults = faults.Where(t => !(t.FAULT_TYPE == "几何尺寸" && t.ALARM_LEVEL == 2));

        //    foreach (string trainName in trainNames)
        //    {
        //        foreach (string siteName in siteNames)
        //        {
        //            IQueryable<TRAIN> s = trains.Where(t => t.NAME == trainName).Where(t => t.SITES.NAME == siteName);
        //            if (s.Count() == 0)
        //            {
        //                continue;
        //            }
        //            StatModel sm = new StatModel();
        //            sm.TRAIN_NAME = trainName;
        //            sm.SITE_NAME = siteName;
        //            sm.TRAIN_CNT = s.Count();
        //            sm.DIMENSION_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.DIMENSION_ALARM2) ?? 0;
        //            sm.DIMENSION_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.DIMENSION_ALARM3) ?? 0;
        //            sm.INSPECTION_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.INSPECTION_ALARM2) ?? 0;
        //            sm.INSPECTION_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.INSPECTION_ALARM3) ?? 0;
        //            sm.SCRAPE_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.SCRAPE_ALARM2) ?? 0;
        //            sm.SCRAPE_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.SCRAPE_ALARM3) ?? 0;
        //            models.Add(sm);
        //        }
        //    }           
        //    return models.AsEnumerable();
        //}

    }
}