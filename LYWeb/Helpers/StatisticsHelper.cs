using LYWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYWeb.Helpers
{
    public class StatisticsHelper
    {
        public static IEnumerable<StatModel> SelectStatByConditions(IQueryable<TRAIN> trains, string CarNumber, string StartTime, string EndTime, string Site)
        {
            DateTime start;
            DateTime end;
            //var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            try
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    start = DateTime.Parse(StartTime);
                    trains = trains.Where(t => t.DETECTION_TIME >= start);
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    end = DateTime.Parse(EndTime);
                    trains = trains.Where(t => t.DETECTION_TIME <= end);
                }
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            if (!string.IsNullOrEmpty(CarNumber))
            {
                string trimedCarNumber = CarNumber.Trim();
                trains = trains.Where(t => t.NAME.EndsWith(trimedCarNumber));
            }
            if (!string.IsNullOrEmpty(Site))
            {
                string trimedSite = Site.Trim();
                trains = trains.Where(t => t.SITES.NAME.EndsWith(trimedSite));
            }
            List<StatModel> result = StatModel.CreateStatModelList(trains);
            StatModel total = StatModel.CalculateTotal(result);
            result = (result.AsEnumerable().OrderByDescending(t => t.TRAIN_CNT).ThenBy(t => t.TRAIN_NAME)).ToList();

            result.Add(total);

            return result.AsEnumerable();
        }

        public static IEnumerable<StatReckecksModel> SelectStatRecheckByConditions(IQueryable<FAULT> Faults, string FaultItem, string StartTime, string EndTime, string Site)
        {
            DateTime start;
            DateTime end;
            //var trains = db.TRAIN.Include(t => t.TRAIN_STATISTICS).Include(t => t.SITES);
            try
            {
                if (!string.IsNullOrEmpty(StartTime))
                {
                    start = DateTime.Parse(StartTime);
                    Faults = Faults.Where(t => t.WHEEL.VEHICLE.TRAIN.DETECTION_TIME >= start);
                }
                if (!string.IsNullOrEmpty(EndTime))
                {
                    end = DateTime.Parse(EndTime);
                    Faults = Faults.Where(t => t.WHEEL.VEHICLE.TRAIN.DETECTION_TIME <= end);
                }
            }
            catch (Exception)
            {
                start = DateTime.MinValue;
                end = DateTime.MaxValue;
            }
            if (!string.IsNullOrEmpty(FaultItem))
            {
                Faults = Faults.Where(t => t.ITEM.EndsWith(FaultItem));
            }
            if (!string.IsNullOrEmpty(Site))
            {
                string trimedSite = Site.Trim();
                Faults = Faults.Where(t => t.WHEEL.VEHICLE.TRAIN.SITES.NAME.EndsWith(trimedSite));
            }
            int a = Faults.Count();
            Faults = Faults.Where(t => !(t.FAULT_TYPE == "几何尺寸" && t.ALARM_LEVEL == 2));
            int b = Faults.Count();
            List<StatReckecksModel> result = StatReckecksModel.CreateStatModelList(Faults);
            StatReckecksModel total = StatReckecksModel.CalculateTotal(result);

            result = (result.AsEnumerable().OrderByDescending(t => t.SITE_NAME).ThenBy(t => t.FAULT_ITEM)).ToList();
            result.Add(total);
            return result.AsEnumerable();
        }
    }
}