using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace LYWeb.Models
{
    [MetadataType(typeof(UNIT_NUMBER_DICTMetaData))]
    public partial class UNIT_NUMBER_DICT
    {

    }
    public class UNIT_NUMBER_DICTMetaData
    {
        public decimal ID { get; set; }
        [Required]
        [DisplayName("车号")]
        public string UNIT_NUMBER { get; set; }
        [DisplayName("配属段")]
        public string DEPOT_NAME { get; set; }
        [DisplayName("车间")]
        public string CHECK_NAME { get; set; }
        [DisplayName("最近过车时间")]
        public Nullable<System.DateTime> LAST_TIME { get; set; }
        [DisplayName("车型")]
        public string CAR_TYPE { get; set; }
        [DisplayName("踏面类型")]
        public string TREAD_TYPE { get; set; }
        [DisplayName("车型")]
        public virtual CAR_TYPE CAR_TYPE1 { get; set; }
        [DisplayName("踏面类型")]
        public virtual TREAD_DICT TREAD_DICT { get; set; }
    }
}