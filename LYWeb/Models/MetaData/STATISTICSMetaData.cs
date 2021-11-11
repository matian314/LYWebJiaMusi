using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{ 
    [MetadataType(typeof(STATISTICSMetaData))]
    public partial class STATISTICS
    {

    }
    public class STATISTICSMetaData
    {
        [Required]
        public string ID { get; set; }
        [DisplayName("年份")]
        public Nullable<short> YEAR { get; set; }
        [DisplayName("月份")]
        public Nullable<byte> MONTH { get; set; }
        [DisplayName("季度")]
        public Nullable<byte> SEASON { get; set; }
        [DisplayName("日期")]
        public Nullable<byte> DAY { get; set; }
        [DisplayName("过车数")]
        public Nullable<int> TRAIN_COUNT { get; set; }
        [DisplayName("报警车数")]
        public Nullable<int> ALARM_COUNT { get; set; }
        [DisplayName("几何尺寸")]
        public Nullable<int> DIMENSION_ALARM_COUNT { get; set; }
        [DisplayName("探伤报警")]
        public Nullable<int> INSPECTION_ALARM_COUNT { get; set; }
        [DisplayName("擦伤报警")]
        public Nullable<int> SCRAPE_ALARM_COUNT { get; set; }
        [DisplayName("一级报警数")]
        public Nullable<int> ALARM_LEVEL1_COUNT { get; set; }
        [DisplayName("二级报警数")]
        public Nullable<int> ALARM_LEVEL2_COUNT { get; set; }
        [DisplayName("三级报警数")]
        public Nullable<int> ALARM_LEVEL3_COUNT { get; set; }
    }
}