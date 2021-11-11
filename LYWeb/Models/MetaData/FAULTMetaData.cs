using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace LYWeb.Models
{
    [MetadataType(typeof(FAULTMetaData))]
    public partial class FAULT
    {

    }
    public class FAULTMetaData
    {
        public string ID { get; set; }
        public string WHEEL_ID { get; set; }
        public string ITEM { get; set; }
        public decimal VALUE { get; set; }
        public bool ISRECHECKED { get; set; }
        public Nullable<bool> ALARM_LEVEL { get; set; }
    }
}