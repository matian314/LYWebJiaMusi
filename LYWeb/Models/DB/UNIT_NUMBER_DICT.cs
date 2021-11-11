namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.UNIT_NUMBER_DICT")]
    public partial class UNIT_NUMBER_DICT
    {
        public decimal ID { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER { get; set; }

        [StringLength(32)]
        public string DEPOT_NAME { get; set; }

        [StringLength(32)]
        public string CHECK_NAME { get; set; }

        public DateTime? LAST_TIME { get; set; }

        [StringLength(36)]
        public string CAR_TYPE { get; set; }

        [StringLength(36)]
        public string TREAD_TYPE { get; set; }

        [StringLength(4)]
        public string COACH0_TYPE { get; set; }

        [StringLength(4)]
        public string COACH1_TYPE { get; set; }

        [StringLength(4)]
        public string COACH2_TYPE { get; set; }

        [StringLength(4)]
        public string COACH3_TYPE { get; set; }

        [StringLength(4)]
        public string COACH4_TYPE { get; set; }

        [StringLength(4)]
        public string COACH5_TYPE { get; set; }

        [StringLength(4)]
        public string COACH6_TYPE { get; set; }

        [StringLength(4)]
        public string COACH7_TYPE { get; set; }

        public virtual CAR_TYPE CAR_TYPE1 { get; set; }

        public virtual TREAD_DICT TREAD_DICT { get; set; }
    }
}
