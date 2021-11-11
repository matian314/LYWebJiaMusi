namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.STATISTICS")]
    public partial class STATISTICS
    {
        [StringLength(36)]
        public string ID { get; set; }

        public short? YEAR { get; set; }

        public byte? MONTH { get; set; }

        public byte? SEASON { get; set; }

        public byte? DAY { get; set; }

        public int? TRAIN_COUNT { get; set; }

        public int? ALARM_COUNT { get; set; }

        public int? DIMENSION_ALARM_COUNT { get; set; }

        public int? INSPECTION_ALARM_COUNT { get; set; }

        public int? SCRAPE_ALARM_COUNT { get; set; }

        public int? ALARM_LEVEL1_COUNT { get; set; }

        public int? ALARM_LEVEL2_COUNT { get; set; }

        public int? ALARM_LEVEL3_COUNT { get; set; }
    }
}
