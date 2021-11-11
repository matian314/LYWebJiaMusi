namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.CHECK_AXLE")]
    public partial class CHECK_AXLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHECK_AXLE()
        {
            CHECK_WHEEL = new HashSet<CHECK_WHEEL>();
            PROBE_DATA = new HashSet<PROBE_DATA>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        public DateTime PASS_TIME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHECK_WHEEL> CHECK_WHEEL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROBE_DATA> PROBE_DATA { get; set; }
    }
}
