namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TheTich")]
    public partial class TheTich
    {
        [Key]
        public int id_thetich { get; set; }

        public int? id_sanpham { get; set; }

        public double? chieucao { get; set; }

        public double? chieurong { get; set; }

        public double? chieudai { get; set; }

        public double? cannang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
