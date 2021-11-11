using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(SITESMetaData))]
    public partial class SITES
    {

    }
    public class SITESMetaData
    {
        [Required]
        public string SITE_ID { get; set; }
        [DisplayName("探测站")]
        public string NAME { get; set; }
        [Required]
        [DisplayName("探测站")]
        public string BUREAU_ID { get; set; }
        [DisplayName("车辆段")]
        public string DEPOT_NAME { get; set; }
        [DisplayName("列检")]
        public string CHECK_NAME { get; set; }
        [DisplayName("车间名")]
        public string PLANT_NAME { get; set; }
        [DisplayName("设备编码")]
        public string DEVICE_ID { get; set; }
        [DisplayName("生产单位")]
        public string FACTORY { get; set; }
    }
}