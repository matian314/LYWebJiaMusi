namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.SCRAPE_DATA")]
    public partial class SCRAPE_DATA
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string WHEEL_ID { get; set; }

        public short? LENGTH { get; set; }

        public string DATA { get; set; }

        public virtual WHEEL WHEEL { get; set; }
    }
}
