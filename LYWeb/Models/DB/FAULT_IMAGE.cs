namespace LYWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TUDS_MANAGER.FAULT_IMAGE")]
    public partial class FAULT_IMAGE
    {
        [StringLength(36)]
        public string ID { get; set; }

        [Required]
        [StringLength(36)]
        public string FAULT_ID { get; set; }

        [StringLength(256)]
        public string PATH { get; set; }

        public virtual FAULT FAULT { get; set; }
    }
}
