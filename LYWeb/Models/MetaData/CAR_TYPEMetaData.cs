using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(CAR_TYPEMetaData))]
    public partial class CAR_TYPE
    {

    }
    public class CAR_TYPEMetaData
    {

        public string ID { get; set; }
        [DisplayName("车型")]
        public string TYPE { get; set; }
        [DisplayName("轴数")]
        public Nullable<byte> AXLE_COUNT { get; set; }
        [DisplayName("轮数")]
        public Nullable<byte> WHEEL_COUNT { get; set; }
        public Nullable<decimal> DIAMETER_UPPERBOUND1 { get; set; }
        public Nullable<decimal> DIAMETER_LOWERBOUND1 { get; set; }
        public Nullable<decimal> DIAMETER_UPPERBOUND2 { get; set; }
        public Nullable<decimal> DIAMETER_LOWERBOUND2 { get; set; }
        public Nullable<decimal> FLANGE_THICKNESS_UPPERBOUND1 { get; set; }
        public Nullable<decimal> FLANGE_THICKNESS_LOWERBOUND1 { get; set; }
        public Nullable<decimal> FLANGE_THICKNESS_UPPERBOUND2 { get; set; }
        public Nullable<decimal> FLANGE_THICKNESS_LOWERBOUND2 { get; set; }
        public Nullable<decimal> FLANGE_HEIGHT_UPPERBOUND1 { get; set; }
        public Nullable<decimal> FLANGE_HEIGHT_LOWERBOUND1 { get; set; }
        public Nullable<decimal> FLANGE_HEIGHT_UPPERBOUND2 { get; set; }
        public Nullable<decimal> FLANGE_HEIGHT_LOWERBOUND2 { get; set; }
        public Nullable<decimal> RIM_THICKNESS_UPPERBOUND1 { get; set; }
        public Nullable<decimal> RIM_THICKNESS_LOWERBOUND1 { get; set; }
        public Nullable<decimal> RIM_THICKNESS_UPPERBOUND2 { get; set; }
        public Nullable<decimal> RIM_THICKNESS_LOWERBOUND2 { get; set; }
        public Nullable<decimal> QR_UPPERBOUND1 { get; set; }
        public Nullable<decimal> QR_LOWERBOUND1 { get; set; }
        public Nullable<decimal> QR_UPPERBOUND2 { get; set; }
        public Nullable<decimal> QR_LOWERBOUND2 { get; set; }
        public Nullable<decimal> THREAD_WEAR_UPPERBOUND1 { get; set; }
        public Nullable<decimal> THREAD_WEAR_LOWERBOUND1 { get; set; }
        public Nullable<decimal> THREAD_WEAR_UPPERBOUND2 { get; set; }
        public Nullable<decimal> THREAD_WEAR_LOWERBOUND2 { get; set; }
        public Nullable<decimal> COXIAL_DIAM_DIFF_UPPERBOUND1 { get; set; }
        public Nullable<decimal> COXIAL_DIAM_DIFF_LOWERBOUND1 { get; set; }
        public Nullable<decimal> COXIAL_DIAM_DIFF_UPPERBOUND2 { get; set; }
        public Nullable<decimal> COXIAL_DIAM_DIFF_LOWERBOUND2 { get; set; }
        public Nullable<decimal> WHEELSET_DISTANCE_UPPERBOUND1 { get; set; }
        public Nullable<decimal> WHEELSET_DISTANCE_LOWERBOUND1 { get; set; }
        public Nullable<decimal> WHEELSET_DISTANCE_UPPERBOUND2 { get; set; }
        public Nullable<decimal> WHEELSET_DISTANCE_LOWERBOUND2 { get; set; }
        public Nullable<decimal> BOGIE_DIAM_DIFF_UPPERBOUND1 { get; set; }
        public Nullable<decimal> BOGIE_DIAM_DIFF_LOWERBOUND1 { get; set; }
        public Nullable<decimal> BOGIE_DIAM_DIFF_UPPERBOUND2 { get; set; }
        public Nullable<decimal> BOGIE_DIAM_DIFF_LOWERBOUND2 { get; set; }
        public Nullable<decimal> VEHICLE_DIAM_DIFF_UPPERBOUND1 { get; set; }
        public Nullable<decimal> VEHICLE_DIAM_DIFF_LOWERBOUND1 { get; set; }
        public Nullable<decimal> VEHICLE_DIAM_DIFF_UPPERBOUND2 { get; set; }
        public Nullable<decimal> VEHICLE_DIAM_DIFF_LOWERBOUND2 { get; set; }
    }
}