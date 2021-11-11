using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(VEHICLEMetaData))]
    public partial class VEHICLE
    {

    }
    public class VEHICLEMetaData
    {
        [Required]
        public string ID { get; set; }
        [DisplayName("车号")]
        public string NAME { get; set; }
        [DisplayName("车号")]
        public string TRAIN_ID { get; set; }
        [DisplayName("车号")]
        public string UNIT_NUMBER { get; set; }
        [DisplayName("车号")]
        public string COACH_NUMBER { get; set; }
        [DisplayName("序号")]
        public Nullable<byte> PASS_ORDER { get; set; }
        [DisplayName("报警级别")]
        public Nullable<byte> ALARMLEVEL { get; set; }
        [DisplayName("探伤级别")]
        public Nullable<byte> INSPECTION_ALARMLEVEL { get; set; }
        [DisplayName("擦伤级别")]
        public Nullable<byte> SCRAPE_ALARMLEVEL { get; set; }
        [DisplayName("几何尺寸")]
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
        [DisplayName("车型")]
        public string CAR_TYPE_ID { get; set; }
        [DisplayName("AEI端位")]
        public string AEI_END_DIRECTION { get; set; }
    }
}