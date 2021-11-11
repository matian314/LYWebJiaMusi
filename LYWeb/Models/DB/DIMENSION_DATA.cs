namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.DIMENSION_DATA")]
    public partial class DIMENSION_DATA
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string WHEEL_ID { get; set; }

        public short? LENGTH { get; set; }

        public string DATA { get; set; }

        public virtual WHEEL WHEEL { get; set; }
    }
}
