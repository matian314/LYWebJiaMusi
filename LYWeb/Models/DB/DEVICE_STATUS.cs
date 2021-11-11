namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.DEVICE_STATUS")]
    public partial class DEVICE_STATUS
    {
        [StringLength(36)]
        public string ID { get; set; }

        public DateTime? CHECK_TIME { get; set; }

        [StringLength(1)]
        public string STATUS { get; set; }

        public decimal? LIQUID_LEVEL { get; set; }

        public decimal? WATER_TEMPERATURE { get; set; }

        public decimal? A_TEMPERATURE { get; set; }

        public decimal? B_TEMPERATURE { get; set; }

        public decimal? ENVIRONMENT_TEMPERATURE { get; set; }

        public decimal? DISK_SPACE { get; set; }

        public DateTime? NEXT_CHECK_TIME { get; set; }
    }
}
