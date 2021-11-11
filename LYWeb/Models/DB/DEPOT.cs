namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.DEPOT")]
    public partial class DEPOT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DEPOT()
        {
            SITES = new HashSet<SITES>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        [StringLength(1)]
        public string BUREAU_ID { get; set; }

        [StringLength(10)]
        public string ABBR { get; set; }

        public virtual BUREAUDICT BUREAUDICT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SITES> SITES { get; set; }
    }
}
