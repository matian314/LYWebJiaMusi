namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.INSPECTION_DEFECT_DATA")]
    public partial class INSPECTION_DEFECT_DATA
    {
        [StringLength(36)]
        public string ID { get; set; }

        public DateTime? DETECTION_TIME { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER { get; set; }

        public byte? WAVE_NUMBER { get; set; }

        public byte? AXLE_INDEX { get; set; }

        [StringLength(12)]
        public string WHEEL_POSITION { get; set; }

        [StringLength(40)]
        public string PROBE_TYPE { get; set; }

        public decimal? PROBE_ANGLE { get; set; }

        public decimal? PROBE_ZERO { get; set; }

        public decimal? SOUND_PATH { get; set; }

        public decimal? DEFECT_AMPLITUDE { get; set; }

        public decimal? WHEEL_ANGLE { get; set; }

        public decimal? DEFECT_DEPTH { get; set; }

        public decimal? WAVE_POINT_INTERVAL { get; set; }

        public string DATA { get; set; }

        public byte? ALARM_LEVEL { get; set; }

        [StringLength(20)]
        public string PROBE_NAME { get; set; }
    }
}
