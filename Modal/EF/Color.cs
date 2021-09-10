namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Color")]
    public partial class Color
    {
        [Key]
        public int id_color { get; set; }

        public int? id_size { get; set; }

        [Column("color")]
        [StringLength(10)]
        public string color1 { get; set; }

        public int? soluong { get; set; }

        public virtual Size Size { get; set; }
    }
}
