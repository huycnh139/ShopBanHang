namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        [Key]
        public int id_slider { get; set; }

        public string image { get; set; }
        [StringLength(100)]
        public string title { get; set; }
        public string description { get; set; }
        [StringLength(50)]
        public string sale { get; set; }
    }
}
