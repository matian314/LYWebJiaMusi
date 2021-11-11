using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(TRAINMetaData))]
    public partial class TRAIN
    {

    }
    public class TRAINMetaData
    {
        [Required]
        public string ID { get; set; }
        [DisplayName("车号")]
        public string NAME { get; set; }
        [DisplayName("轴数")]
        public Nullable<byte> AXLE_COUNT { get; set; }
        [DisplayName("探测时间")]
        public System.DateTime DETECTION_TIME { get; set; }
        [DisplayName("担当局")]
        public string BUREAUDICT_ID { get; set; }
        [DisplayName("报警级别")]
        public Nullable<byte> ALARM_LEVEL { get; set; }
        [DisplayName("探伤报警")]
        public Nullable<byte> INSPECTION_ALARM_LEVEL { get; set; }
        [DisplayName("擦伤报警")]
        public Nullable<byte> SCRAPE_ALARM_LEVEL { get; set; }
        [DisplayName("几何尺寸")]
        public Nullable<byte> DIMENSION_ALARM_LEVEL { get; set; }
        [DisplayName("车轮直径")]
        public Nullable<byte> DIAMETER_ALARM_LEVEL { get; set; }
        [DisplayName("轮缘厚度")]
        public Nullable<byte> FLANGE_THICKNESS_ALARM_LEVEL { get; set; }
        [DisplayName("轮缘高度")]
        public Nullable<byte> FLANGE_HEIGHT_ALARM_LEVEL { get; set; }
        [DisplayName("轮辋厚度")]
        public Nullable<byte> RIM_THICKNESS_ALARM_LEVEL { get; set; }
        [DisplayName("QR值")]
        public Nullable<byte> QR_ALARMLEVEL { get; set; }
        [DisplayName("车号")]
        public string UNIT_NUMBER1 { get; set; }
        [DisplayName("端位")]
        public string END_POSITION { get; set; }
        [DisplayName("车站")]
        public string SITE_ID { get; set; }
        [DisplayName("操作员")]
        public string OPERATOR { get; set; }
        public Nullable<bool> CHECKED { get; set; }
        [DisplayName("最快速度")]
        public Nullable<decimal> MAX_SPEED { get; set; }
        [DisplayName("最慢速度")]
        public Nullable<decimal> MIN_SPEED { get; set; }
        [DisplayName("车型")]
        public string TRAIN_TYPE { get; set; }
        [DisplayName("车辆数")]
        public Nullable<byte> VEHICLE_COUNT { get; set; }
        [DisplayName("车号")]
        public string TRAIN_NO { get; set; }
        [DisplayName("过车速度")]
        public Nullable<decimal> SPEED { get; set; }
        [DisplayName("探测站")]
        public string DEPOT { get; set; }
    }
}