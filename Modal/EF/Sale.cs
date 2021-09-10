namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sale")]
    public partial class Sale
    {
        [Key]
        public int id_sale { get; set; }

        public int? id_sanpham { get; set; }

        public DateTime? thoigianbd { get; set; }

        public DateTime? thoigiankt { get; set; }

        [Column("sale")]
        public int? sale1 { get; set; }

        public DateTime? thoigianc { get; set; }

        [StringLength(50)]
        public string trangthai { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
