namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.SITES")]
    public partial class SITES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SITES()
        {
            TRAIN = new HashSet<TRAIN>();
        }

        [Key]
        [StringLength(36)]
        public string SITE_ID { get; set; }

        [StringLength(40)]
        public string NAME { get; set; }

        [StringLength(10)]
        public string CODE { get; set; }

        [StringLength(1)]
        public string BUREAU_ID { get; set; }

        [StringLength(20)]
        public string DEPOT_NAME { get; set; }

        [StringLength(20)]
        public string CHECK_NAME { get; set; }

        [StringLength(20)]
        public string PLANT_NAME { get; set; }

        [StringLength(36)]
        public string DEVICE_ID { get; set; }

        [StringLength(40)]
        public string FACTORY { get; set; }

        [StringLength(40)]
        public string DEVICE_NAME { get; set; }

        [StringLength(36)]
        public string DEPOT_ID { get; set; }

        public virtual DEPOT DEPOT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAIN> TRAIN { get; set; }
    }
}
