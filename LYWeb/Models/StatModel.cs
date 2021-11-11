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
        [DisplayName("编组")]
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

        public static IEnumerable<StatModel> CreateStatModelList(IQueryable<TRAIN> trains)
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
            return models.AsEnumerable();
        }

    }
}