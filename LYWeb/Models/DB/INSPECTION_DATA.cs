namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.INSPECTION_DATA")]
    public partial class INSPECTION_DATA
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string TRAIN_ID { get; set; }

        public string DATA { get; set; }

        [StringLength(10)]
        public string PROBE_ORDER { get; set; }

        public virtual TRAIN TRAIN { get; set; }
    }
}
