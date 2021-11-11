namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.INSPECTION_DEVICE_STATE")]
    public partial class INSPECTION_DEVICE_STATE
    {
        [StringLength(36)]
        public string ID { get; set; }

        public DateTime? DETECTION_TIME { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER { get; set; }

        [StringLength(80)]
        public string CARD_YW { get; set; }

        [StringLength(80)]
        public string CARD_YN { get; set; }

        [StringLength(80)]
        public string CARD_ZN { get; set; }

        [StringLength(80)]
        public string CARD_ZW { get; set; }

        [StringLength(640)]
        public string PROBE_YW { get; set; }

        [StringLength(640)]
        public string PROBE_YN { get; set; }

        [StringLength(640)]
        public string PROBE_ZN { get; set; }

        [StringLength(640)]
        public string PROBE_ZW { get; set; }
    }
}
