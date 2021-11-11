namespace LYWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TudsEntities : DbContext
    {
        public TudsEntities()
            : base("name=TudsEntities")
        {
        }

        public virtual DbSet<BUREAUDICT> BUREAUDICT { get; set; }
        public virtual DbSet<CAR_TYPE> CAR_TYPE { get; set; }
        public virtual DbSet<CHECK_AXLE> CHECK_AXLE { get; set; }
        public virtual DbSet<CHECK_WHEEL> CHECK_WHEEL { get; set; }
        public virtual DbSet<DEPOT> DEPOT { get; set; }
        public virtual DbSet<DEVICE_STATUS> DEVICE_STATUS { get; set; }
        public virtual DbSet<FAULT> FAULT { get; set; }
        public virtual DbSet<FAULT_IMAGE> FAULT_IMAGE { get; set; }
        public virtual DbSet<INSPECTION_DATA> INSPECTION_DATA { get; set; }
        public virtual DbSet<INSPECTION_DEFECT_DATA> INSPECTION_DEFECT_DATA { get; set; }
        public virtual DbSet<INSPECTION_DEVICE_STATE> INSPECTION_DEVICE_STATE { get; set; }
        public virtual DbSet<PROBE_DATA> PROBE_DATA { get; set; }
        public virtual DbSet<RECHECKS> RECHECKS { get; set; }
        public virtual DbSet<SCRAPE_DATA> SCRAPE_DATA { get; set; }
        public virtual DbSet<SCRAPE_DEVICE_STATUS> SCRAPE_DEVICE_STATUS { get; set; }
        public virtual DbSet<SITES> SITES { get; set; }
        public virtual DbSet<STATISTICS> STATISTICS { get; set; }
        public virtual DbSet<TRAIN> TRAIN { get; set; }
        public virtual DbSet<TRAIN_STATISTICS> TRAIN_STATISTICS { get; set; }
        public virtual DbSet<TREAD_DICT> TREAD_DICT { get; set; }
        public virtual DbSet<UNIT_NUMBER_DICT> UNIT_NUMBER_DICT { get; set; }
        public virtual DbSet<UPLOAD_EMIS> UPLOAD_EMIS { get; set; }
        public virtual DbSet<VEHICLE> VEHICLE { get; set; }
        public virtual DbSet<VEHICLE_STATISTICS> VEHICLE_STATISTICS { get; set; }
        public virtual DbSet<WEBUSER> WEBUSER { get; set; }
        public virtual DbSet<WHEEL> WHEEL { get; set; }
        public virtual DbSet<DIMENSION_DATA> DIMENSION_DATA { get; set; }
        public virtual DbSet<LOGINUSER> LOGINUSER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BUREAUDICT>()
                .Property(e => e.BUREAU_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BUREAUDICT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<BUREAUDICT>()
                .Property(e => e.ABBR)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BUREAUDICT>()
                .HasMany(e => e.DEPOT)
                .WithOptional(e => e.BUREAUDICT)
                .WillCascadeOnDelete();

            modelBuilder.Entity<BUREAUDICT>()
                .HasMany(e => e.TRAIN)
                .WithOptional(e => e.BUREAUDICT)
                .HasForeignKey(e => e.BUREAUDICT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.DIAMETER_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.DIAMETER_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.DIAMETER_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.DIAMETER_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.RIM_THICKNESS_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.RIM_THICKNESS_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.RIM_THICKNESS_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.RIM_THICKNESS_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.QR_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.QR_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.QR_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.QR_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.THREAD_WEAR_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.THREAD_WEAR_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.THREAD_WEAR_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.THREAD_WEAR_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.COXIAL_DIAM_DIFF_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.COXIAL_DIAM_DIFF_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.COXIAL_DIAM_DIFF_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.COXIAL_DIAM_DIFF_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.WHEELSET_DISTANCE_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.WHEELSET_DISTANCE_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.WHEELSET_DISTANCE_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.WHEELSET_DISTANCE_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BOGIE_DIAM_DIFF_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BOGIE_DIAM_DIFF_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BOGIE_DIAM_DIFF_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BOGIE_DIAM_DIFF_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.VEHICLE_DIAM_DIFF_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.VEHICLE_DIAM_DIFF_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.VEHICLE_DIAM_DIFF_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.VEHICLE_DIAM_DIFF_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_UPPERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_LOWERBOUND1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_UPPERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_LOWERBOUND2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FORWARD_COACHES)
                .IsUnicode(false);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BACKWARD_COACHES)
                .IsUnicode(false);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_UPPERBOUND1_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_LOWERBOUND1_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_UPPERBOUND2_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_LENGTH_LOWERBOUND2_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_UPPERBOUND1_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_LOWERBOUND1_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_UPPERBOUND2_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.BRUISE_DEPTH_LOWERBOUND2_S)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.VEHICLE_DIAM_DIFF_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.SCRAPE_DEPTH_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.COAXIAL_DIAM_DIFF_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.WHEELSET_DISTANCE_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.THREAD_WEAR_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.QR_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.RIM_THICKNESS_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_HEIGHT_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.FLANGE_THICKNESS_STANDARD)
                .HasPrecision(7, 3);

            modelBuilder.Entity<CAR_TYPE>()
                .Property(e => e.DIAMETER_STANDARD)
                .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_DIAMETER_LOWERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_DIAMETER_LOWERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_DIAMETER_UPPERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_DIAMETER_UPPERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_BOGIE_DIFF_LOWERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_BOGIE_DIFF_LOWERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_BOGIE_DIFF_UPPERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_BOGIE_DIFF_UPPERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_VEH_DIFF_LOWERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_VEH_DIFF_LOWERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_VEH_DIFF_UPPERBOUND1)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_VEH_DIFF_UPPERBOUND2)
            //    .HasPrecision(7, 3);

            //modelBuilder.Entity<CAR_TYPE>()
            //    .Property(e => e.TRAILER_COACHES)
            //    .IsUnicode(false);

            modelBuilder.Entity<CAR_TYPE>()
                .HasMany(e => e.UNIT_NUMBER_DICT)
                .WithOptional(e => e.CAR_TYPE1)
                .HasForeignKey(e => e.CAR_TYPE);

            modelBuilder.Entity<CAR_TYPE>()
                .HasMany(e => e.VEHICLE)
                .WithOptional(e => e.CAR_TYPE1)
                .HasForeignKey(e => e.CAR_TYPE_ID);

            modelBuilder.Entity<DEPOT>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<DEPOT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<DEPOT>()
                .Property(e => e.BUREAU_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DEPOT>()
                .Property(e => e.ABBR)
                .IsUnicode(false);

            modelBuilder.Entity<DEPOT>()
                .HasMany(e => e.SITES)
                .WithOptional(e => e.DEPOT)
                .HasForeignKey(e => e.DEPOT_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.LIQUID_LEVEL)
                .HasPrecision(38, 0);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.WATER_TEMPERATURE)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.A_TEMPERATURE)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.B_TEMPERATURE)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.ENVIRONMENT_TEMPERATURE)
                .HasPrecision(3, 1);

            modelBuilder.Entity<DEVICE_STATUS>()
                .Property(e => e.DISK_SPACE)
                .HasPrecision(6, 3);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.WHEEL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.ITEM)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.VALUE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.FAULT_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.UNIT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.COACH_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .Property(e => e.WHEEL_POSITION)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT>()
                .HasMany(e => e.FAULT_IMAGE)
                .WithRequired(e => e.FAULT)
                .HasForeignKey(e => e.FAULT_ID);

            modelBuilder.Entity<FAULT>()
                .HasMany(e => e.RECHECKS)
                .WithRequired(e => e.FAULT)
                .HasForeignKey(e => e.FAULT_ID);

            modelBuilder.Entity<FAULT_IMAGE>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT_IMAGE>()
                .Property(e => e.FAULT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<FAULT_IMAGE>()
                .Property(e => e.PATH)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DATA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DATA>()
                .Property(e => e.TRAIN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DATA>()
                .Property(e => e.PROBE_ORDER)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.UNIT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.WHEEL_POSITION)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.PROBE_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.PROBE_ANGLE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.PROBE_ZERO)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.SOUND_PATH)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.DEFECT_AMPLITUDE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.WHEEL_ANGLE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.DEFECT_DEPTH)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.WAVE_POINT_INTERVAL)
                .HasPrecision(7, 3);

            modelBuilder.Entity<INSPECTION_DEFECT_DATA>()
                .Property(e => e.PROBE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.UNIT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.CARD_YW)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.CARD_YN)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.CARD_ZN)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.CARD_ZW)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.PROBE_YW)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.PROBE_YN)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.PROBE_ZN)
                .IsUnicode(false);

            modelBuilder.Entity<INSPECTION_DEVICE_STATE>()
                .Property(e => e.PROBE_ZW)
                .IsUnicode(false);

            modelBuilder.Entity<PROBE_DATA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<PROBE_DATA>()
                .Property(e => e.TRAIN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<PROBE_DATA>()
                .Property(e => e.LINE)
                .IsUnicode(false);

            modelBuilder.Entity<PROBE_DATA>()
                .Property(e => e.PROBE_ZERO)
                .HasPrecision(8, 3);

            modelBuilder.Entity<PROBE_DATA>()
                .Property(e => e.WAVE_PT_INTV)
                .HasPrecision(8, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.FAULT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.HANDLER)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.SUGGESTION)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.CONCLUSION)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.RECHECK_PERSON)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.STATE)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.REMARKS)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE1)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE2)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE3)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE4)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE5)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE6)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE7)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.VALUE8)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.DIFF_VALUE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.ERROR_ANALYSIS)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.FOREMAN)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.QUALITY_INSPECTOR)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.RECHECK_HANDLER)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.TEAM_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<RECHECKS>()
                .Property(e => e.DISPATCHER)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DATA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DATA>()
                .Property(e => e.WHEEL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.PLC)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.UPS)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR1)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR2)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR3)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR4)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR5)
                .IsUnicode(false);

            modelBuilder.Entity<SCRAPE_DEVICE_STATUS>()
                .Property(e => e.SENSOR6)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.SITE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.CODE)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.BUREAU_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.DEPOT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.CHECK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.PLANT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.DEVICE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.FACTORY)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.DEVICE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .Property(e => e.DEPOT_ID)
                .IsUnicode(false);

            modelBuilder.Entity<SITES>()
                .HasMany(e => e.TRAIN)
                .WithOptional(e => e.SITES)
                .WillCascadeOnDelete();

            modelBuilder.Entity<STATISTICS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.BUREAUDICT_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.UNIT_NUMBER1)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.END_POSITION)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.SITE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.OPERATOR)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.MAX_SPEED)
                .HasPrecision(4, 2);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.MIN_SPEED)
                .HasPrecision(4, 2);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.TRAIN_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.TRAIN_NO)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.DEPOT)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.SPEED)
                .HasPrecision(4, 2);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.SITE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .Property(e => e.DATA_SOURCE_STATUS)
                .IsUnicode(false);

            modelBuilder.Entity<TRAIN>()
                .HasMany(e => e.INSPECTION_DATA)
                .WithRequired(e => e.TRAIN)
                .HasForeignKey(e => e.TRAIN_ID);

            modelBuilder.Entity<TRAIN>()
                .HasMany(e => e.PROBE_DATA)
                .WithRequired(e => e.TRAIN)
                .HasForeignKey(e => e.TRAIN_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TRAIN>()
                .HasOptional(e => e.TRAIN_STATISTICS)
                .WithRequired(e => e.TRAIN)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TRAIN>()
                .HasMany(e => e.VEHICLE)
                .WithRequired(e => e.TRAIN)
                .HasForeignKey(e => e.TRAIN_ID);

            modelBuilder.Entity<TRAIN_STATISTICS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND1_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND1_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND2_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND2_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND1_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND1_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_UPPERBOUND2_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_THICKNESS_LOWERBOUND2_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND1_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND1_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND2_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND2_L)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND1_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND1_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_UPPERBOUND2_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .Property(e => e.FLANGE_HEIGHT_LOWERBOUND2_S)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TREAD_DICT>()
                .HasMany(e => e.UNIT_NUMBER_DICT)
                .WithOptional(e => e.TREAD_DICT)
                .HasForeignKey(e => e.TREAD_TYPE);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.UNIT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.DEPOT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.CHECK_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.CAR_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.TREAD_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH0_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH1_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH2_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH3_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH4_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH5_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH6_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UNIT_NUMBER_DICT>()
                .Property(e => e.COACH7_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<UPLOAD_EMIS>()
                .Property(e => e.ID)
                .HasPrecision(38, 0);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.TRAIN_ID)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.UNIT_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.COACH_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.CAR_TYPE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.AEI_END_DIRECTION)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.SPEED)
                .HasPrecision(4, 2);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.COACH_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.CAR_TYPE)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.DEPOT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .Property(e => e.SITE_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<VEHICLE>()
                .HasOptional(e => e.VEHICLE_STATISTICS)
                .WithRequired(e => e.VEHICLE)
                .WillCascadeOnDelete();

            modelBuilder.Entity<VEHICLE>()
                .HasMany(e => e.WHEEL)
                .WithRequired(e => e.VEHICLE)
                .HasForeignKey(e => e.VEHICLE_ID);

            modelBuilder.Entity<VEHICLE_STATISTICS>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.REAL_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.EMPLOYEE_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.BUREAU_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.DEPOT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.ROLE)
                .IsUnicode(false);

            modelBuilder.Entity<WEBUSER>()
                .Property(e => e.UNIT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.VEHICLE_ID)
                .IsUnicode(false);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.POSITION)
                .IsUnicode(false);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.DIAMETER)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.FLANGE_THICKNESS)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.FLANGE_HEIGHT)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.RIM_THICKNESS)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.QR)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.COAXIAL_DIAMETER_DIFFERENCE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.TREAD_WEAR)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.WHEELSET_DISTANCE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.BOGIE_DIAMETER_DIFFERENCE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.VEHICLE_DIAMETER_DIFFERENCE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.PASS_SPEED)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.ROUND)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.MAX_BRUISE_LENGTH)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.MAX_BRUISE_DEPTH)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.MAX_DIAMETER)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.MIN_DIAMETER)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.ET)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.OR_VALUE)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .Property(e => e.VERTICAL_WEAR)
                .HasPrecision(7, 3);

            modelBuilder.Entity<WHEEL>()
                .HasMany(e => e.FAULT)
                .WithRequired(e => e.WHEEL)
                .HasForeignKey(e => e.WHEEL_ID);

            modelBuilder.Entity<WHEEL>()
                .HasMany(e => e.SCRAPE_DATA)
                .WithRequired(e => e.WHEEL)
                .HasForeignKey(e => e.WHEEL_ID);

            modelBuilder.Entity<WHEEL>()
                .HasMany(e => e.DIMENSION_DATA)
                .WithRequired(e => e.WHEEL)
                .HasForeignKey(e => e.WHEEL_ID);

            modelBuilder.Entity<DIMENSION_DATA>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<DIMENSION_DATA>()
                .Property(e => e.WHEEL_ID)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.NAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.REAL_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.EMPLOYEE_NUMBER)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.BUREAU_ID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.DEPOT_NAME)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.PASSWORD)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.ROLE)
                .IsUnicode(false);

            modelBuilder.Entity<LOGINUSER>()
                .Property(e => e.UNIT_NAME)
                .IsUnicode(false);
				
            modelBuilder.Entity<CHECK_AXLE>()
    .Property(e => e.ID)
    .IsUnicode(false);

            modelBuilder.Entity<CHECK_AXLE>()
                .HasMany(e => e.CHECK_WHEEL)
                .WithRequired(e => e.CHECK_AXLE)
                .HasForeignKey(e => e.CHECK_AXLE_ID);

            modelBuilder.Entity<CHECK_AXLE>()
                .HasMany(e => e.PROBE_DATA)
                .WithOptional(e => e.CHECK_AXLE)
                .HasForeignKey(e => e.AXLE_ID);

				
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.ID)
                .IsUnicode(false);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.NAME)
                .IsUnicode(false);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.CHECK_AXLE_ID)
                .IsUnicode(false);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.POSITION)
                .IsUnicode(false);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.DIAMETER)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.FLANGE_THICKNESS)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.FLANGE_HEIGHT)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.RIM_THICKNESS)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.QR)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.COAXIAL_DIAMETER_DIFFERENCE)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.TREAD_WEAR)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.WHEELSET_DISTANCE)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.PASS_SPEED)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.ROUND)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.MAX_BRUISE_LENGTH)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.MAX_BRUISE_DEPTH)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.MAX_DIAMETER)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.MIN_DIAMETER)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.ET)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.OR_VALUE)
                .HasPrecision(7, 3);
            modelBuilder.Entity<CHECK_WHEEL>()
                .Property(e => e.VERTICAL_WEAR)
                .HasPrecision(7, 3);
        }
    }
}
