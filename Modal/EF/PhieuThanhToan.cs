namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuThanhToan")]
    public partial class PhieuThanhToan
    {
        [Key]
        public int id_phieutt { get; set; }

        public int? id_hoadon { get; set; }

        [StringLength(250)]
        public string tensp { get; set; }

        public int? soluong { get; set; }

        public double? dongia { get; set; }

        public DateTime? thoigian { get; set; }

        [StringLength(50)]
        public string trangthai { get; set; }
    }
}
