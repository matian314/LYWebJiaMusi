using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LYWeb.Models
{
    [MetadataType(typeof(BUREAUDICTMetaData))]
    public partial class BUREAUDICT
    {

    }

    public class BUREAUDICTMetaData
    {
        [Required]
        [DisplayName("所属局名")]
        public string BUREAU_ID { get; set; }
        [DisplayName("担当局")]
        public string NAME { get; set; }
        [DisplayName("担当局")]
        public string ABBR { get; set; }
        public byte SEQUENCE_NUMBER { get; set; }
    }
}