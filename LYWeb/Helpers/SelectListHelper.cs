using LYWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LYWeb.Helpers
{
    public static class SelectListHelper
    {
        public static IEnumerable<TRAIN> GetTrainByAlarmLevel(IEnumerable<TRAIN> trains, string alarmLevel, string dataSource, string faultItem)
        {
            var predicate = CreatePredicateForAlarmlevel(alarmLevel);
            switch (dataSource)
            {
                case "DIMENSION_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.DIMENSION_ALARM_LEVEL));
                    break;
                case "INSPECTION_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.INSPECTION_ALARM_LEVEL));
                    break;
                case "SCRAPE_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.SCRAPE_ALARM_LEVEL));
                    break;
                case "ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.ALARM_LEVEL));
                    break;
                default:
                    throw new ArgumentException(nameof(dataSource));
            }
            switch (faultItem)
            {
                case "ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.ALARM_LEVEL));
                    break;
                case "DIAMETER_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.DIAMETER_ALARM_LEVEL));
                    break;
                case "FLANGE_HEIGHT_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.FLANGE_HEIGHT_ALARM_LEVEL));
                    break;
                case "FLANGE_THICKNESS_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.FLANGE_THICKNESS_ALARM_LEVEL));
                    break;
                case "RIM_THICKNESS_ALARM_LEVEL":
                    trains = trains.Where(t => predicate(t.RIM_THICKNESS_ALARM_LEVEL));
                    break;
                case "QR_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.QR_ALARMLEVEL));
                    break;
                case "TREAD_WEAR_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.TREAD_WEAR_ALARMLEVEL));
                    break;
                case "WHEELSET_DISTANCE_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.WHEELSET_DISTANCE_ALARMLEVEL));
                    break;
                case "VEHICLE_DIAM_DIFF_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.VEHICLE_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "BOGIE_DIAM_DIFF_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.BOGIE_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "COAXIAL_DIAM_DIFF_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.COAXIAL_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "BRUISE_DEPTH_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.BRUISE_DEPTH_ALARMLEVEL));
                    break;
                case "BRUISE_LENGTH_ALARMLEVEL":
                    trains = trains.Where(t => predicate(t.BRUISE_LENGTH_ALARMLEVEL));
                    break;
                default:
                    throw new ArgumentException(nameof(faultItem));
            }
            return trains;
        }

        internal static IEnumerable<FAULT> GetFaultByAlarmLevel(IEnumerable<FAULT> faults, string alarmLevel, string dataSource, string faultItem)
        {
            switch(alarmLevel)
            {
                case "123":
                case "23":
                    break;
                case "2":
                    faults = faults.Where(f => f.ALARM_LEVEL == 2);
                    break;
                case "3":
                    faults = faults.Where(f => f.ALARM_LEVEL == 3);
                    break;
                default:
                    throw new ArgumentException(nameof(alarmLevel));
            }
            switch (dataSource)
            {
                case "DIMENSION_ALARM_LEVEL":
                    faults = faults.Where(f => f.FAULT_TYPE == "几何尺寸");
                    break;
                case "INSPECTION_ALARM_LEVEL":
                    faults = faults.Where(t => t.FAULT_TYPE == "探伤");
                    break;
                case "SCRAPE_ALARM_LEVEL":
                    faults = faults.Where(t => t.FAULT_TYPE == "擦伤");
                    break;
                case "ALARM_LEVEL":
                    break;
                default:
                    throw new ArgumentException(nameof(dataSource));
            }
            switch(faultItem)
            {
                case "ALARM_LEVEL":
                    break;
                case "DIAMETER_ALARM_LEVEL":
                    faults = faults.Where(t => t.ITEM == "车轮直径");
                    break;
                case "FLANGE_HEIGHT_ALARM_LEVEL":
                    faults = faults.Where(t => t.ITEM == "轮缘高度");
                    break;
                case "FLANGE_THICKNESS_ALARM_LEVEL":
                    faults = faults.Where(t => t.ITEM == "轮缘厚度");
                    break;
                case "RIM_THICKNESS_ALARM_LEVEL":
                    faults = faults.Where(t => t.ITEM == "轮辋厚度");
                    break;
                case "QR_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "QR值");
                    break;
                case "TREAD_WEAR_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "踏面磨耗");
                    break;
                case "WHEELSET_DISTANCE_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "内侧距");
                    break;
                case "VEHICLE_DIAM_DIFF_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "同车轮径差");
                    break;
                case "BOGIE_DIAM_DIFF_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "同转向架轮径差");
                    break;
                case "COAXIAL_DIAM_DIFF_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "同轴轮径差");
                    break;
                case "BRUISE_DEPTH_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "擦伤深度");
                    break;
                case "BRUISE_LENGTH_ALARMLEVEL":
                    faults = faults.Where(t => t.ITEM == "擦伤长度");
                    break;
                default:
                    throw new ArgumentException(nameof(faultItem));
            }
            return faults;
        }

        public static IEnumerable<WHEEL> GetWheelByAlarmLevel(IEnumerable<WHEEL> wheels, string alarmLevel, string dataSource, string faultItem)
        {
            var predicate = CreatePredicateForAlarmlevel(alarmLevel);
            switch (dataSource)
            {
                case "DIMENSION_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.DIMENSION_ALARMLEVEL));
                    break;
                case "INSPECTION_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.INSPECTION_ALARMLEVEL));
                    break;
                case "SCRAPE_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.SCRAPE_ALARMLEVEL));
                    break;
                case "ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.ALARMLEVEL));
                    break;
                default:
                    throw new ArgumentException(nameof(dataSource));
            }
            switch (faultItem)
            {
                case "ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.ALARMLEVEL));
                    break;
                case "DIAMETER_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.DIAMETER_ALARMLEVEL));
                    break;
                case "FLANGE_HEIGHT_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.FLANGE_HEIGHT_ALARMLEVEL));
                    break;
                case "FLANGE_THICKNESS_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.FLANGE_THICKNESS_ALARMLEVEL));
                    break;
                case "RIM_THICKNESS_ALARM_LEVEL":
                    wheels = wheels.Where(t => predicate(t.RIM_THICKNESS_ALARMLEVEL));
                    break;
                case "QR_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.QR_ALARMLEVEL));
                    break;
                case "TREAD_WEAR_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.TREAD_WEAR_ALARMLEVEL));
                    break;
                case "WHEELSET_DISTANCE_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.WHEELSET_DISTANCE_ALARMLEVEL));
                    break;
                case "VEHICLE_DIAM_DIFF_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.VEHICLE_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "BOGIE_DIAM_DIFF_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.BOGIE_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "COAXIAL_DIAM_DIFF_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.COAXIAL_DIAM_DIFF_ALARMLEVEL));
                    break;
                case "BRUISE_DEPTH_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.BRUISE_DEPTH_ALARMLEVEL));
                    break;
                case "BRUISE_LENGTH_ALARMLEVEL":
                    wheels = wheels.Where(t => predicate(t.BRUISE_LENGTH_ALARMLEVEL));
                    break;
                default:
                    throw new ArgumentException(nameof(faultItem));
            }
            return wheels;
        }
        public static IEnumerable<WEBUSER> GetWEBUSER(IEnumerable<WEBUSER> WEBUSER)
        {
            return WEBUSER;
        }
        public static Predicate<short?> CreatePredicateForAlarmlevel(string value)
        {
            if (value == "123")
            {
                return new Predicate<short?>(a => a <= 3 && a >= 1);
            }
            if (value == "2")
            {
                return new Predicate<short?>(a => a == 2);
            }
            if (value == "3")
            {
                return new Predicate<short?>(a => a == 3);
            }
            if(value == "23")
            {
                return new Predicate<short?>(a => a == 3 || a == 2);
            }
            throw new ArgumentException(nameof(value));
        }
    }
}