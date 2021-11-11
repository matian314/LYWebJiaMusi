namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.CAR_TYPE")]
    public partial class CAR_TYPE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAR_TYPE()
        {
            UNIT_NUMBER_DICT = new HashSet<UNIT_NUMBER_DICT>();
            VEHICLE = new HashSet<VEHICLE>();
        }

        [StringLength(36)]
        public string ID { get; set; }

        [StringLength(20)]
        public string TYPE { get; set; }

        public byte? AXLE_COUNT { get; set; }

        public byte? WHEEL_COUNT { get; set; }

        public decimal? DIAMETER_UPPERBOUND1 { get; set; }

        public decimal? DIAMETER_LOWERBOUND1 { get; set; }

        public decimal? DIAMETER_UPPERBOUND2 { get; set; }

        public decimal? DIAMETER_LOWERBOUND2 { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND1 { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND1 { get; set; }

        public decimal? FLANGE_THICKNESS_UPPERBOUND2 { get; set; }

        public decimal? FLANGE_THICKNESS_LOWERBOUND2 { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND1 { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND1 { get; set; }

        public decimal? FLANGE_HEIGHT_UPPERBOUND2 { get; set; }

        public decimal? FLANGE_HEIGHT_LOWERBOUND2 { get; set; }

        public decimal? RIM_THICKNESS_UPPERBOUND1 { get; set; }

        public decimal? RIM_THICKNESS_LOWERBOUND1 { get; set; }

        public decimal? RIM_THICKNESS_UPPERBOUND2 { get; set; }

        public decimal? RIM_THICKNESS_LOWERBOUND2 { get; set; }

        public decimal? QR_UPPERBOUND1 { get; set; }

        public decimal? QR_LOWERBOUND1 { get; set; }

        public decimal? QR_UPPERBOUND2 { get; set; }

        public decimal? QR_LOWERBOUND2 { get; set; }

        public decimal? THREAD_WEAR_UPPERBOUND1 { get; set; }

        public decimal? THREAD_WEAR_LOWERBOUND1 { get; set; }

        public decimal? THREAD_WEAR_UPPERBOUND2 { get; set; }

        public decimal? THREAD_WEAR_LOWERBOUND2 { get; set; }

        public decimal? COXIAL_DIAM_DIFF_UPPERBOUND1 { get; set; }

        public decimal? COXIAL_DIAM_DIFF_LOWERBOUND1 { get; set; }

        public decimal? COXIAL_DIAM_DIFF_UPPERBOUND2 { get; set; }

        public decimal? COXIAL_DIAM_DIFF_LOWERBOUND2 { get; set; }

        public decimal? WHEELSET_DISTANCE_UPPERBOUND1 { get; set; }

        public decimal? WHEELSET_DISTANCE_LOWERBOUND1 { get; set; }

        public decimal? WHEELSET_DISTANCE_UPPERBOUND2 { get; set; }

        public decimal? WHEELSET_DISTANCE_LOWERBOUND2 { get; set; }

        public decimal? BOGIE_DIAM_DIFF_UPPERBOUND1 { get; set; }

        public decimal? BOGIE_DIAM_DIFF_LOWERBOUND1 { get; set; }

        public decimal? BOGIE_DIAM_DIFF_UPPERBOUND2 { get; set; }

        public decimal? BOGIE_DIAM_DIFF_LOWERBOUND2 { get; set; }

        public decimal? VEHICLE_DIAM_DIFF_UPPERBOUND1 { get; set; }

        public decimal? VEHICLE_DIAM_DIFF_LOWERBOUND1 { get; set; }

        public decimal? VEHICLE_DIAM_DIFF_UPPERBOUND2 { get; set; }

        public decimal? VEHICLE_DIAM_DIFF_LOWERBOUND2 { get; set; }

        public decimal? BRUISE_LENGTH_UPPERBOUND1 { get; set; }

        public decimal? BRUISE_LENGTH_LOWERBOUND1 { get; set; }

        public decimal? BRUISE_LENGTH_UPPERBOUND2 { get; set; }

        public decimal? BRUISE_LENGTH_LOWERBOUND2 { get; set; }

        public decimal? BRUISE_DEPTH_UPPERBOUND1 { get; set; }

        public decimal? BRUISE_DEPTH_LOWERBOUND1 { get; set; }

        public decimal? BRUISE_DEPTH_UPPERBOUND2 { get; set; }

        public decimal? BRUISE_DEPTH_LOWERBOUND2 { get; set; }

        [StringLength(80)]
        public string FORWARD_COACHES { get; set; }

        [StringLength(80)]
        public string BACKWARD_COACHES { get; set; }

        public decimal? BRUISE_LENGTH_UPPERBOUND1_S { get; set; }

        public decimal? BRUISE_LENGTH_LOWERBOUND1_S { get; set; }

        public decimal? BRUISE_LENGTH_UPPERBOUND2_S { get; set; }

        public decimal? BRUISE_LENGTH_LOWERBOUND2_S { get; set; }

        public decimal? BRUISE_DEPTH_UPPERBOUND1_S { get; set; }

        public decimal? BRUISE_DEPTH_LOWERBOUND1_S { get; set; }

        public decimal? BRUISE_DEPTH_UPPERBOUND2_S { get; set; }

        public decimal? BRUISE_DEPTH_LOWERBOUND2_S { get; set; }

        public decimal? VEHICLE_DIAM_DIFF_STANDARD { get; set; }

        public decimal? SCRAPE_DEPTH_STANDARD { get; set; }

        public decimal? COAXIAL_DIAM_DIFF_STANDARD { get; set; }

        public decimal? WHEELSET_DISTANCE_STANDARD { get; set; }

        public decimal? THREAD_WEAR_STANDARD { get; set; }

        public decimal? QR_STANDARD { get; set; }

        public decimal? RIM_THICKNESS_STANDARD { get; set; }

        public decimal? FLANGE_HEIGHT_STANDARD { get; set; }

        public decimal? FLANGE_THICKNESS_STANDARD { get; set; }

        public decimal? DIAMETER_STANDARD { get; set; }

        //public decimal? TRAILER_DIAMETER_LOWERBOUND1 { get; set; }

        //public decimal? TRAILER_DIAMETER_LOWERBOUND2 { get; set; }

        //public decimal? TRAILER_DIAMETER_UPPERBOUND1 { get; set; }

        //public decimal? TRAILER_DIAMETER_UPPERBOUND2 { get; set; }

        //public decimal? TRAILER_BOGIE_DIFF_LOWERBOUND1 { get; set; }

        //public decimal? TRAILER_BOGIE_DIFF_LOWERBOUND2 { get; set; }

        //public decimal? TRAILER_BOGIE_DIFF_UPPERBOUND1 { get; set; }

        //public decimal? TRAILER_BOGIE_DIFF_UPPERBOUND2 { get; set; }

        //public decimal? TRAILER_VEH_DIFF_LOWERBOUND1 { get; set; }

        //public decimal? TRAILER_VEH_DIFF_LOWERBOUND2 { get; set; }

        //public decimal? TRAILER_VEH_DIFF_UPPERBOUND1 { get; set; }

        //public decimal? TRAILER_VEH_DIFF_UPPERBOUND2 { get; set; }

        //[StringLength(40)]
        //public string TRAILER_COACHES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UNIT_NUMBER_DICT> UNIT_NUMBER_DICT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VEHICLE> VEHICLE { get; set; }
    }
}
