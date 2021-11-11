namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.UPLOAD_EMIS")]
    public partial class UPLOAD_EMIS
    {
        public decimal ID { get; set; }

        public DateTime? LATEST_TRAIN_TIME { get; set; }

        public DateTime? LATEST_RECHECK_TIME { get; set; }
    }
}
