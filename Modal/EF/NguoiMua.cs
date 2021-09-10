namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiMua")]
    public partial class NguoiMua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NguoiMua()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        public int id_nguoimua { get; set; }

        public int? id_taikhoan { get; set; }

        public int phone { get; set; }

        [StringLength(100)]
        public string street { get; set; }

        [StringLength(100)]
        public string ward { get; set; }

        [StringLength(100)]
        public string district { get; set; }

        [StringLength(100)]
        public string province { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
