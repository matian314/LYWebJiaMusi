namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.TREAD_DICT")]
    public partial class TREAD_DICT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TREAD_DICT()
        {
            UNIT_NUMBER_DICT = new HashSet<UNIT_NUMBER_DICT>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND1_L { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND1_L { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND2_L { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND2_L { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND1_S { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND1_S { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND2_S { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND2_S { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND1_L { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND1_L { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND2_L { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND2_L { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND1_S { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND1_S { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND2_S { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND2_S { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UNIT_NUMBER_DICT> UNIT_NUMBER_DICT { get; set; }
    }
}
