namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.TRAIN")]
    public partial class TRAIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAIN()
        {
            INSPECTION_DATA = new HashSet<INSPECTION_DATA>();
            PROBE_DATA = new HashSet<PROBE_DATA>();
            VEHICLE = new HashSet<VEHICLE>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(40)]
        public string NAME { get; set; }

        public byte? AXLE_COUNT { get; set; }

        public DateTime DETECTION_TIME { get; set; }

        [StringLength(1)]
        public string BUREAUDICT_ID { get; set; }

        public byte? ALARM_LEVEL { get; set; }

        public byte? INSPECTION_ALARM_LEVEL { get; set; }

        public byte? SCRAPE_ALARM_LEVEL { get; set; }

        public byte? DIMENSION_ALARM_LEVEL { get; set; }

        public byte? DIAMETER_ALARM_LEVEL { get; set; }

        public byte? FLANGE_THICKNESS_ALARM_LEVEL { get; set; }

        public byte? FLANGE_HEIGHT_ALARM_LEVEL { get; set; }

        public byte? RIM_THICKNESS_ALARM_LEVEL { get; set; }

        public byte? QR_ALARMLEVEL { get; set; }

        [StringLength(20)]
        public string UNIT_NUMBER1 { get; set; }

        [StringLength(1)]
        public string END_POSITION { get; set; }

        [StringLength(36)]
        public string SITE_ID { get; set; }

        public byte? TREAD_WEAR_ALARMLEVEL { get; set; }

        [StringLength(20)]
        public string OPERATOR { get; set; }

        public bool? CHECKED { get; set; }

        public decimal? MAX_SPEED { get; set; }

        public decimal? MIN_SPEED { get; set; }

        [StringLength(1)]
        public string TRAIN_TYPE { get; set; }

        public byte? VEHICLE_COUNT { get; set; }

        [StringLength(10)]
        public string TRAIN_NO { get; set; }

        public byte? VERTICAL_WEAR_ALARMLEVEL { get; set; }

        public byte? COAXIAL_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? BOGIE_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? VEHICLE_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? WHEELSET_DISTANCE_ALARMLEVEL { get; set; }

        public byte? BRUISE_DEPTH_ALARMLEVEL { get; set; }

        public byte? BRUISE_LENGTH_ALARMLEVEL { get; set; }

        [StringLength(40)]
        public string DEPOT { get; set; }

        public decimal? SPEED { get; set; }

        //public byte? PANTOGRAPH_ALARMLEVEL { get; set; }

        //public byte? SKATEBOARD_ALARMLEVEL { get; set; }

        [StringLength(40)]
        public string SITE_NAME { get; set; }

        public bool? DIMENSION_CONTAINED { get; set; }

        public bool? INSPECTION_CONTAINED { get; set; }

        public bool? SCRAPE_CONTAINED { get; set; }

        public bool? AEI_CONTAINED { get; set; }
        public bool? ALERT_PLAYED { get; set; }

        [StringLength(100)]
        public string DATA_SOURCE_STATUS { get; set; }

        public virtual BUREAUDICT BUREAUDICT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INSPECTION_DATA> INSPECTION_DATA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PROBE_DATA> PROBE_DATA { get; set; }

        public virtual SITES SITES { get; set; }

        public virtual TRAIN_STATISTICS TRAIN_STATISTICS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEHICLE> VEHICLE { get; set; }
    }
}
