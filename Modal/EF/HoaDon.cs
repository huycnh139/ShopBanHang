namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            GHN_Ship = new HashSet<GHN_Ship>();
            HoaDonCTs = new HashSet<HoaDonCT>();
        }

        [Key]
        public int id_hoadon { get; set; }

        public int? id_sanpham { get; set; }

        public int? id_loaisp { get; set; }

        public int? id_nguoimua { get; set; }

        [StringLength(100)]
        public string mahd { get; set; }

        public double? tonggia { get; set; }

        public DateTime thoigian { get; set; }

        [StringLength(100)]
        public string hinhthuctt { get; set; }

        public double? freeship { get; set; }

        [StringLength(50)]
        public string trangthai { get; set; }

        public int? soluong { get; set; }

        public double? sotiendathanhtoan { get; set; }

        [StringLength(50)]
        public string magiaodich { get; set; }

        public bool? duyet { get; set; }

        [StringLength(100)]
        public string maghn { get; set; }

        public string trangthaigh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GHN_Ship> GHN_Ship { get; set; }

        public virtual NguoiMua NguoiMua { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonCT> HoaDonCTs { get; set; }
    }
}
