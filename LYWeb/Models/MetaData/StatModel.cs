using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LYWeb.Models
{
    public class StatModel
    {
        [DisplayName("车号")]
        public string TRAIN_NAME { get; set; }
        [DisplayName("探测点")]
        public string SITE_NAME { get; set; }
        [DisplayName("过车数")]
        public Nullable<int> TRAIN_CNT { get; set; }
        [DisplayName("探伤二级报警数")]
        public Nullable<int> INSPECTION_CNT2 { get; set; }
        [DisplayName("探伤三级报警数")]
        public Nullable<int> INSPECTION_CNT3 { get; set; }
        [DisplayName("擦伤二级报警数")]
        public Nullable<int> SCRAPE_CNT2 { get; set; }
        [DisplayName("擦伤三级报警数")]
        public Nullable<int> SCRAPE_CNT3 { get; set; }
        [DisplayName("几何尺寸二级报警数")]
        public Nullable<int> DIMENSION_CNT2 { get; set; }
        [DisplayName("几何尺寸三级报警数")]
        public Nullable<int> DIMENSION_CNT3 { get; set; }

        public static List<StatModel> CreateStatModelList(IQueryable<TRAIN> trains)
        {
            List<StatModel> models = new List<StatModel>();
            List<string> trainNames = trains.Select(t => t.NAME).Distinct().ToList();
            List<string> siteNames = trains.Select(t => t.SITES.NAME).Distinct().ToList();
            foreach (string trainName in trainNames)
            {
                foreach (string siteName in siteNames)
                {
                    IQueryable<TRAIN> s = trains.Where(t => t.NAME == trainName).Where(t => t.SITES.NAME == siteName);
                    if (s.Count() == 0)
                    {
                        continue;
                    }
                    StatModel sm = new StatModel();
                    sm.TRAIN_NAME = trainName;
                    sm.SITE_NAME = siteName;
                    sm.TRAIN_CNT = s.Count();
                    sm.DIMENSION_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.DIMENSION_ALARM2) ?? 0;
                    sm.DIMENSION_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.DIMENSION_ALARM3) ?? 0;
                    sm.INSPECTION_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.INSPECTION_ALARM2) ?? 0;
                    sm.INSPECTION_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.INSPECTION_ALARM3) ?? 0;
                    sm.SCRAPE_CNT2 = s.Sum(t => t.TRAIN_STATISTICS.SCRAPE_ALARM2) ?? 0;
                    sm.SCRAPE_CNT3 = s.Sum(t => t.TRAIN_STATISTICS.SCRAPE_ALARM3) ?? 0;
                    models.Add(sm);
                }
            }
            CalculateTotal(models);
            //return models.AsEnumerable();
            return models;
        }

        public static StatModel CalculateTotal(List<StatModel> models)
        {
            int totalTrain = 0;
            int totalDimension2 = 0;
            int totalDimension3 = 0;
            int totalInspection2 = 0;
            int totalInspection3 = 0;
            int totalScrape2 = 0;
            int totalScrape3 = 0;
            foreach (StatModel sm in models)
            {
                totalTrain += sm.TRAIN_CNT ?? 0;
                totalDimension2 += sm.DIMENSION_CNT2 ?? 0;
                totalDimension3 += sm.DIMENSION_CNT3 ?? 0;
                totalInspection2 += sm.INSPECTION_CNT2 ?? 0;
                totalInspection3 += sm.INSPECTION_CNT3 ?? 0;
                totalScrape2 += sm.SCRAPE_CNT2 ?? 0;
                totalScrape3 += sm.SCRAPE_CNT3 ?? 0;
            }
            StatModel total = new StatModel();
            total.TRAIN_NAME = "总计";
            total.SITE_NAME = "--";
            total.TRAIN_CNT = totalTrain;
            total.DIMENSION_CNT2 = totalDimension2;
            total.DIMENSION_CNT3 = totalDimension3;
            total.INSPECTION_CNT2 = totalInspection2;
            total.INSPECTION_CNT3 = totalInspection3;
            total.SCRAPE_CNT2 = totalScrape2;
            total.SCRAPE_CNT3 = totalScrape3;
            return total;
        }
    }
}