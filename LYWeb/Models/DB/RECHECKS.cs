namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.RECHECKS")]
    public partial class RECHECKS
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string FAULT_ID { get; set; }

        [StringLength(20)]
        public string HANDLER { get; set; }

        [StringLength(400)]
        public string SUGGESTION { get; set; }

        [StringLength(400)]
        public string CONCLUSION { get; set; }

        public DateTime? DEALT_TIME { get; set; }

        [StringLength(40)]
        public string VALUE { get; set; }

        [StringLength(20)]
        public string RECHECK_PERSON { get; set; }

        [StringLength(200)]
        public string STATE { get; set; }

        public DateTime? RECHECKED_TIME { get; set; }

        [StringLength(200)]
        public string REMARKS { get; set; }

        public decimal? VALUE1 { get; set; }

        public decimal? VALUE2 { get; set; }

        public decimal? VALUE3 { get; set; }

        public decimal? VALUE4 { get; set; }

        public decimal? VALUE5 { get; set; }

        public decimal? VALUE6 { get; set; }

        public decimal? VALUE7 { get; set; }

        public decimal? VALUE8 { get; set; }

        public decimal? DIFF_VALUE { get; set; }

        [StringLength(40)]
        public string ERROR_ANALYSIS { get; set; }

        [StringLength(20)]
        public string FOREMAN { get; set; }

        [StringLength(20)]
        public string QUALITY_INSPECTOR { get; set; }

        [StringLength(20)]
        public string RECHECK_HANDLER { get; set; }

        [StringLength(20)]
        public string TEAM_NAME { get; set; }

        [StringLength(20)]
        public string DISPATCHER { get; set; }

        public virtual FAULT FAULT { get; set; }
    }
}
