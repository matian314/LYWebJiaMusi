namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.FAULT")]
    public partial class FAULT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FAULT()
        {
            FAULT_IMAGE = new HashSet<FAULT_IMAGE>();
            RECHECKS = new HashSet<RECHECKS>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string WHEEL_ID { get; set; }

        [StringLength(20)]
        public string ITEM { get; set; }

        public decimal? VALUE { get; set; }

        public bool? ISRECHECKED { get; set; }

        public byte? ALARM_LEVEL { get; set; }

        [StringLength(10)]
        public string FAULT_TYPE { get; set; }

        public bool? IS_COMPLETED { get; set; }

        public DateTime? DETECTION_TIME { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER { get; set; }

        [StringLength(20)]
        public string COACH_NUMBER { get; set; }

        [StringLength(20)]
        public string WHEEL_POSITION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAULT_IMAGE> FAULT_IMAGE { get; set; }

        public virtual WHEEL WHEEL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECHECKS> RECHECKS { get; set; }
    }
}
