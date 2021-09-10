namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDonCT")]
    public partial class HoaDonCT
    {
        [Key]
        public int id_hoadonct { get; set; }

        public int? id_hoadon { get; set; }

        public int? id_sanpham { get; set; }

        public double? dongia { get; set; }

        public DateTime thoigian { get; set; }

        public int? soluong { get; set; }

        [StringLength(50)]
        public string trangthai { get; set; }

        [StringLength(10)]
        public string color { get; set; }

        public int? id_size { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
