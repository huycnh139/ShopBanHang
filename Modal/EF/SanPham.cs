namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            DauGias = new HashSet<DauGia>();
            HoaDonCTs = new HashSet<HoaDonCT>();
            Sales = new HashSet<Sale>();
            Sizes = new HashSet<Size>();
            TheTiches = new HashSet<TheTich>();
        }

        [Key]
        public int id_sanpham { get; set; }

        [StringLength(200)]
        public string tensp { get; set; }

        public int? id_loaisp { get; set; }

        [StringLength(50)]
        public string tenngan { get; set; }

        public int? soluong { get; set; }

        public double dongia { get; set; }

        public double giasale { get; set; }

        [StringLength(5)]
        public string trangthai { get; set; }

        [StringLength(5)]
        public string hienthi { get; set; }

        [StringLength(100)]
        public string barcode { get; set; }

        [StringLength(10)]
        public string tinhtrang { get; set; }

        public string thongtin { get; set; }

        public string hinhanh1 { get; set; }

        public string hinhanh2 { get; set; }

        public string hinhanh3 { get; set; }

        public string hinhanh4 { get; set; }

        [StringLength(2)]
        public string size { get; set; }

        [StringLength(100)]
        public string xeploai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DauGia> DauGias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonCT> HoaDonCTs { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sale> Sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Size> Sizes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TheTich> TheTiches { get; set; }
    }
}
