namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.CHECK_WHEEL")]
    public partial class CHECK_WHEEL
    {
        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(40)]
        public string NAME { get; set; }

        [Required]
        [StringLength(36)]
        public string CHECK_AXLE_ID { get; set; }

        [StringLength(6)]
        public string POSITION { get; set; }

        public byte? ALARMLEVEL { get; set; }

        public byte? INSPECTION_ALARMLEVEL { get; set; }

        public byte? SCRAPE_ALARMLEVEL { get; set; }

        public byte? DIMENSION_ALARMLEVEL { get; set; }

        public byte? DIAMETER_ALARMLEVEL { get; set; }

        public byte? FLANGE_THICKNESS_ALARMLEVEL { get; set; }

        public byte? FLANGE_HEIGHT_ALARMLEVEL { get; set; }

        public byte? RIM_THICKNESS_ALARMLEVEL { get; set; }

        public byte? QR_ALARMLEVEL { get; set; }

        public decimal? DIAMETER { get; set; }

        public decimal? FLANGE_THICKNESS { get; set; }

        public decimal? FLANGE_HEIGHT { get; set; }

        public decimal? RIM_THICKNESS { get; set; }

        public decimal? QR { get; set; }

        public decimal? COAXIAL_DIAMETER_DIFFERENCE { get; set; }

        public decimal? TREAD_WEAR { get; set; }

        public decimal? WHEELSET_DISTANCE { get; set; }

        public decimal? PASS_SPEED { get; set; }

        public decimal? ROUND { get; set; }

        public decimal? MAX_BRUISE_LENGTH { get; set; }

        public decimal? MAX_BRUISE_DEPTH { get; set; }

        public decimal? MAX_DIAMETER { get; set; }

        public decimal? MIN_DIAMETER { get; set; }

        public decimal? ET { get; set; }

        public decimal? OR_VALUE { get; set; }

        public decimal? VERTICAL_WEAR { get; set; }

        public byte? VERTICAL_WEAR_ALARMLEVEL { get; set; }

        public byte? TREAD_WEAR_ALARMLEVEL { get; set; }

        public byte? COAXIAL_DIAM_DIFF_ALARMLEVEL { get; set; }

        public byte? BRUISE_LENGTH_ALARMLEVEL { get; set; }

        public byte? BRUISE_DEPTH_ALARMLEVEL { get; set; }

        public byte? WHEELSET_DISTANCE_ALARMLEVEL { get; set; }

        public virtual CHECK_AXLE CHECK_AXLE { get; set; }
    }
}
