using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(RECHECKSMetaData))]
    public partial class RECHECKS
    {

    }
    public class RECHECKSMetaData
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string FAULT_ID { get; set; }
        [DisplayName("处理人")]
        public string HANDLER { get; set; }
        [DisplayName("分析意见")]
        public string SUGGESTION { get; set; }
        [DisplayName("处理结论")]
        public string CONCLUSION { get; set; }
        [DisplayName("处理时间")]
        public System.DateTime DEALT_TIME { get; set; }
        [DisplayName("符合值")]
        public Nullable<decimal> VALUE { get; set; }
        [DisplayName("复核人")]
        public string RECHECK_PERSON { get; set; }
        [DisplayName("当前状态")]
        public string STATE { get; set; }
        [DisplayName("复核时间")]
        public System.DateTime RECHECKED_TIME { get; set; }
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        [DisplayName("轮位2")]
        public Nullable<decimal> VALUE2 { get; set; }
        [DisplayName("轮位3")]
        public Nullable<decimal> VALUE3 { get; set; }
        [DisplayName("轮位4")]
        public Nullable<decimal> VALUE4 { get; set; }
        [DisplayName("轮位5")]
        public Nullable<decimal> VALUE5 { get; set; }
        [DisplayName("轮位6")]
        public Nullable<decimal> VALUE6 { get; set; }
        [DisplayName("轮位7")]
        public Nullable<decimal> VALUE7 { get; set; }
        [DisplayName("轮位8")]
        public Nullable<decimal> VALUE8 { get; set; }
        [DisplayName("误差分析")]
        public string ERROR_ANALYSIS { get; set; }
        [DisplayName("差值")]
        public Nullable<decimal> DIFF_VALUE { get; set; }
        [DisplayName("轮位1")]
        public Nullable<decimal> VALUE1 { get; set; }
        [DisplayName("班组长")]
        public string FOREMAN { get; set; }
        [DisplayName("质检员")]
        public string QUALITY_INSPECTOR { get; set; }
        [DisplayName("处理人")]
        //复核时的处理人
        public string RECHECK_HANDLER { get; set; }
        [DisplayName("班组名")]
        public string TEAM_NAME { get; set; }
        [DisplayName("现场调度员")]
        public string DISPATCHER { get; set; }
        public virtual FAULT FAULT { get; set; }
    }
}