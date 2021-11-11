using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LYWeb.Models
{
    [MetadataType(typeof(WHEELMetaData))]
    public partial class WHEEL
    {

    }
    public class WHEELMetaData
    {
        [Required]
        public string ID { get; set; }
        [DisplayName("轮号")]
        public string NAME { get; set; }
        [Required]
        public string VEHICLE_ID { get; set; }
        [DisplayName("轮位")]
        public string POSITION { get; set; }
        [DisplayName("轴位")]
        public byte AXLE_POSITION { get; set; }
        [DisplayName("报警级别")]
        public Nullable<byte> ALARMLEVEL { get; set; }
        [DisplayName("探伤级别")]
        public Nullable<byte> INSPECTION_ALARMLEVEL { get; set; }
        [DisplayName("擦伤级别")]
        public Nullable<byte> SCRAPE_ALARMLEVEL { get; set; }
        [DisplayName("几何尺寸级别")]
        public Nullable<byte> DIMENSION_ALARMLEVEL { get; set; }
        [DisplayName("车轮直径")]
        public Nullable<byte> DIAMETER_ALARMLEVEL { get; set; }
        [DisplayName("轮缘厚度")]
        public Nullable<byte> FLANGE_THICKNESS_ALARMLEVEL { get; set; }
        [DisplayName("轮缘高度")]
        public Nullable<byte> FLANGE_HEIGHT_ALARMLEVEL { get; set; }
        [DisplayName("轮辋厚度")]
        public Nullable<byte> RIM_THICKNESS_ALARMLEVEL { get; set; }
        [DisplayName("QR值")]
        public Nullable<byte> QR_ALARMLEVEL { get; set; }
        [DisplayName("车轮直径")]
        public Nullable<decimal> DIAMETER { get; set; }
        [DisplayName("轮缘厚度")]
        public Nullable<decimal> FLANGE_THICKNESS { get; set; }
        [DisplayName("轮缘高度")]
        public Nullable<decimal> FLANGE_HEIGHT { get; set; }
        [DisplayName("轮辋厚度")]
        public Nullable<decimal> RIM_THICKNESS { get; set; }
        [DisplayName("QR值")]
        public Nullable<decimal> QR { get; set; }
        [DisplayName("同轴轮径差")]
        public Nullable<decimal> COAXIAL_DIAMETER_DIFFERENCE { get; set; }
        [DisplayName("踏面磨耗")]
        public Nullable<decimal> TREAD_WEAR { get; set; }
        [DisplayName("内侧距")]
        public Nullable<decimal> WHEELSET_DISTANCE { get; set; }
        [DisplayName("同转向架轮径差")]
        public Nullable<decimal> BOGIE_DIAMETER_DIFFERENCE { get; set; }
        [DisplayName("同车轮径差")]
        public Nullable<decimal> VEHICLE_DIAMETER_DIFFERENCE { get; set; }
        [DisplayName("速度")]
        public Nullable<decimal> PASS_SPEED { get; set; }
        [DisplayName("径跳")]
        public Nullable<decimal> ROUND { get; set; }
        [DisplayName("擦伤长度")]
        public Nullable<decimal> MAX_BRUISE_LENGTH { get; set; }
        [DisplayName("擦伤深度")]
        public Nullable<decimal> MAX_BRUISE_DEPTH { get; set; }
        [DisplayName("最大轮径")]
        public Nullable<decimal> MAX_DIAMETER { get; set; }
        [DisplayName("最小轮径")]
        public Nullable<decimal> MIN_DIAMETER { get; set; }
        [DisplayName("轮序")]
        public Nullable<byte> WHEEL_ORDER { get; set; }
        [DisplayName("等效斜度")]
        public Nullable<decimal> ET { get; set; }
        [DisplayName("轮缘斜度")]
        public Nullable<decimal> OR_VALUE { get; set; }
    }
}