namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.WEBUSER")]
    public partial class WEBUSER
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(40)]
        public string NAME { get; set; }

        [Required]
        [StringLength(40)]
        public string REAL_NAME { get; set; }

        [Required]
        [StringLength(20)]
        public string EMPLOYEE_NUMBER { get; set; }

        [StringLength(1)]
        public string BUREAU_ID { get; set; }

        [StringLength(40)]
        public string DEPOT_NAME { get; set; }

        public DateTime? REGISTER_TIME { get; set; }

        public DateTime? LATEST_LOGIN_TIME { get; set; }

        public byte? LOGINFAILED_COUNTER { get; set; }

        [Required]
        [StringLength(256)]
        public string PASSWORD { get; set; }

        public DateTime? LOCKOUT_TIME { get; set; }

        public bool? ENABLED { get; set; }

        [StringLength(20)]
        public string ROLE { get; set; }

        [StringLength(40)]
        public string UNIT_NAME { get; set; }

        public virtual BUREAUDICT BUREAUDICT { get; set; }
    }
}
