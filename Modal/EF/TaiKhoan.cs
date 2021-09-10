namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            LichSuDGs = new HashSet<LichSuDG>();
            NguoiBans = new HashSet<NguoiBan>();
            NguoiMuas = new HashSet<NguoiMua>();
        }

        [Key]
        public int id_taikhoan { get; set; }

        [StringLength(250)]
        public string username { get; set; }

        [StringLength(250)]
        public string password { get; set; }

        [StringLength(250)]
        public string password_old { get; set; }

        [StringLength(250)]
        public string email { get; set; }

        public bool loaitk { get; set; }

        [StringLength(150)]
        public string hoten { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuDG> LichSuDGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiBan> NguoiBans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiMua> NguoiMuas { get; set; }
    }
}
