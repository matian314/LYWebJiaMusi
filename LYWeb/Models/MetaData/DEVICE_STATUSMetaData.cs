using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LYWeb.Models
{
    [MetadataType(typeof(DEVICE_STATUSMetaData))]
    public partial class DEVICE_STATUS
    {

    }
    public class DEVICE_STATUSMetaData
    {
        [Required]
        public string ID { get; set; }
        [DisplayName("检测时间")]
        public Nullable<System.DateTime> CHECK_TIME { get; set; }
        [DisplayName("设备状态")]
        public string STATUS { get; set; }
        [DisplayName("液位")]
        public Nullable<decimal> LIQUID_LEVEL { get; set; }
        [DisplayName("水温")]
        public Nullable<decimal> WATER_TEMPERATURE { get; set; }
        [DisplayName("A温度")]
        public Nullable<decimal> A_TEMPERATURE { get; set; }
        [DisplayName("B温度")]
        public Nullable<decimal> B_TEMPERATURE { get; set; }
        [DisplayName("环境温度")]
        public Nullable<decimal> ENVIRONMENT_TEMPERATURE { get; set; }
        [DisplayName("磁盘空间")]
        public Nullable<decimal> DISK_SPACE { get; set; }
        [DisplayName("下次检测时间")]
        public Nullable<System.DateTime> NEXT_CHECK_TIME { get; set; }
    }
}