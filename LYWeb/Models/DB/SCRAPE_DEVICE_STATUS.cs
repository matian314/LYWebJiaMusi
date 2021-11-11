namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.SCRAPE_DEVICE_STATUS")]
    public partial class SCRAPE_DEVICE_STATUS
    {
        [StringLength(36)]
        public string ID { get; set; }

        public DateTime? TIME { get; set; }

        [StringLength(1)]
        public string PLC { get; set; }

        [StringLength(1)]
        public string UPS { get; set; }

        [StringLength(1)]
        public string SENSOR1 { get; set; }

        [StringLength(1)]
        public string SENSOR2 { get; set; }

        [StringLength(1)]
        public string SENSOR3 { get; set; }

        [StringLength(1)]
        public string SENSOR4 { get; set; }

        [StringLength(1)]
        public string SENSOR5 { get; set; }

        [StringLength(1)]
        public string SENSOR6 { get; set; }
    }
}
