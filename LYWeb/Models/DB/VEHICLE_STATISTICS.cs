namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.VEHICLE_STATISTICS")]
    public partial class VEHICLE_STATISTICS
    {
        [StringLength(36)]
        public string ID { get; set; }

        public short? INSPECTION_ALARM2 { get; set; }

        public short? INSPECTION_ALARM3 { get; set; }

        public short? SCRAPE_ALARM2 { get; set; }

        public short? SCRAPE_ALARM3 { get; set; }

        public short? DIMENSION_ALARM2 { get; set; }

        public short? DIMENSION_ALARM3 { get; set; }

        public short? ALARM2 { get; set; }

        public short? ALARM3 { get; set; }

        public short? FLANGE_HIGHT_ALARM2 { get; set; }

        public short? FLANGE_HIGHT_ALARM3 { get; set; }

        public short? FLANGE_THICKNESS_ALARM2 { get; set; }

        public short? FLANGE_THICKNESS_ALARM3 { get; set; }

        public short? RIM_THICKNESS_ALARM2 { get; set; }

        public short? RIM_THICKNESS_ALARM3 { get; set; }

        public short? TREAD_WEAR_ALARM2 { get; set; }

        public short? TREAD_WEAR_ALARM3 { get; set; }

        public short? VERTICAL_WEAR_ALARM1 { get; set; }

        public short? VERTICAL_WEAR_ALARM2 { get; set; }

        public short? VERTICAL_WEAR_ALARM3 { get; set; }

        public short? QR_ALARM2 { get; set; }

        public short? QR_ALARM3 { get; set; }

        public short? WHEELSET_ALARM2 { get; set; }

        public short? WHEELSET_ALARM3 { get; set; }

        public short? COAXIAL_DIAMETER_DIFF_ALARM2 { get; set; }

        public short? COAXIAL_DIAMETER_DIFF_ALARM3 { get; set; }

        public short? BOGIE_DIAMETER_DIFF_ALARM2 { get; set; }

        public short? BOGIE_DIAMETER_DIFF_ALARM3 { get; set; }

        public short? VEHICLE_DIAMETER_DIFF_ALARM2 { get; set; }

        public short? VEHICLE_DIAMETER_DIFF_ALARM3 { get; set; }

        public short? BRUISE_LENGTH_ALARM2 { get; set; }

        public short? BRUISE_DEPTH_ALARM2 { get; set; }

        public short? BRUISE_LENGTH_ALARM3 { get; set; }

        public short? BRUISE_DEPTH_ALARM3 { get; set; }

        public short? DIAMETER_ALARM2 { get; set; }

        public short? DIAMETER_ALARM3 { get; set; }

        public virtual VEHICLE VEHICLE { get; set; }
    }
}
