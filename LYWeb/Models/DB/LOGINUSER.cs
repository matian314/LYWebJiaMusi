namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.LOGINUSER")]
    public partial class LOGINUSER
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(36)]
        public string ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public string NAME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public string REAL_NAME { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string EMPLOYEE_NUMBER { get; set; }

        [StringLength(1)]
        public string BUREAU_ID { get; set; }

        [StringLength(40)]
        public string DEPOT_NAME { get; set; }

        public DateTime? REGISTER_TIME { get; set; }

        public DateTime? LATEST_LOGIN_TIME { get; set; }

        public byte? LOGINFAILED_COUNTER { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(256)]
        public string PASSWORD { get; set; }

        public DateTime? LOCKOUT_TIME { get; set; }

        public bool? ENABLED { get; set; }

        [StringLength(20)]
        public string ROLE { get; set; }

        [StringLength(40)]
        public string UNIT_NAME { get; set; }
    }
}
