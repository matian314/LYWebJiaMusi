namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.BUREAUDICT")]
    public partial class BUREAUDICT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BUREAUDICT()
        {
            DEPOT = new HashSet<DEPOT>();
            TRAIN = new HashSet<TRAIN>();
            WEBUSER = new HashSet<WEBUSER>();
        }

        [Key]
        [StringLength(1)]
        public string BUREAU_ID { get; set; }

        [Required]
        [StringLength(20)]
        public string NAME { get; set; }

        [Required]
        [StringLength(2)]
        public string ABBR { get; set; }

        public byte SEQUENCE_NUMBER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DEPOT> DEPOT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRAIN> TRAIN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WEBUSER> WEBUSER { get; set; }
    }
}
