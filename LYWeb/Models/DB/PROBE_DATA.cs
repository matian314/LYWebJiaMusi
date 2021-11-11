namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.PROBE_DATA")]
    public partial class PROBE_DATA
    {
        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(36)]
        public string TRAIN_ID { get; set; }

        public short? PROBE_INDEX { get; set; }

        [StringLength(4)]
        public string LINE { get; set; }

        public byte? STATUS { get; set; }

        public int? TYPE { get; set; }

        public byte? DB { get; set; }

        public short? SOUND_PATH { get; set; }

        public decimal? PROBE_ZERO { get; set; }

        public decimal? WAVE_PT_INTV { get; set; }

        public short? AXLE_COUNT { get; set; }

        public string DETAIL_DATA { get; set; }

        [StringLength(36)]
        public string AXLE_ID { get; set; }
        public virtual CHECK_AXLE CHECK_AXLE { get; set; }
        public virtual TRAIN TRAIN { get; set; }
    }
}
