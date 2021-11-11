namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.VEHICLE")]
    public partial class VEHICLE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VEHICLE()
        {
            WHEEL = new HashSet<WHEEL>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(20)]
        public string NAME { get; set; }

        [Required]
        [StringLength(36)]
        public string TRAIN_ID { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER { get; set; }

        [StringLength(20)]
        public string COACH_NUMBER { get; set; }

        public byte? PASS_ORDER { get; set; }

        public byte? ALARMLEVEL { get; set; }

        public byte? INSPECTION_ALARMLEVEL { get; set; }

        public byte? SCRAPE_ALARMLEVEL { get; set; }

        public byte? DIMENSION_ALARMLEVEL { get; set; }

        public byte? DIAMETER_ALARMLEVEL { get; set; }

        public byte? FLANGE_THICKNESS_ALARMLEVEL { get; set; }

        public byte? FLANGE_HEIGHT_ALARMLEVEL { get; set; }

        public byte? RIM_THICKNESS_ALARMLEVEL { get; set; }

        public byte? QR_ALARMLEVEL { get; set; }

        public byte? TREAD_WEAR_ALARMLEVEL { get; set; }

        [StringLength(36)]
        public string CAR_TYPE_ID { get; set; }

        [StringLength(1)]
        public string AEI_END_DIRECTION { get; set; }

        public byte? VERTICAL_WEAR_ALARMLEVEL { get; set; }

        public byte? BRUISE_LENGTH_ALARMLEVEL { get; set; }

        public byte? BRUISE_DEPTH_ALARMLEVEL { get; set; }

        public byte? COAXIAL_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? BOGIE_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? VEHICLE_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? AXLE_COUNT { get; set; }

        public byte? WHEEL_COUNT { get; set; }

        public byte? WHEELSET_DISTANCE_ALARMLEVEL { get; set; }

        public decimal? SPEED { get; set; }

        [StringLength(4)]
        public string COACH_TYPE { get; set; }

        [StringLength(40)]
        public string CAR_TYPE { get; set; }

        [StringLength(40)]
        public string DEPOT_NAME { get; set; }

        [StringLength(40)]
        public string SITE_NAME { get; set; }

        public DateTime? DETECTION_TIME { get; set; }

        public virtual CAR_TYPE CAR_TYPE1 { get; set; }

        public virtual TRAIN TRAIN { get; set; }

        public virtual VEHICLE_STATISTICS VEHICLE_STATISTICS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WHEEL> WHEEL { get; set; }
    }
}
