namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DauGia")]
    public partial class DauGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DauGia()
        {
            LichSuDGs = new HashSet<LichSuDG>();
        }

        [Key]
        public int id_daugia { get; set; }

        public int? id_sanpham { get; set; }

        [StringLength(20)]
        public string status_ { get; set; }
        public string status_use { get; set; }


        public DateTime? time_start { get; set; }

        public DateTime time_end { get; set; }

        public DateTime? time_left { get; set; }

        [StringLength(20)]
        public string result { get; set; }

        public virtual SanPham SanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichSuDG> LichSuDGs { get; set; }
    }
}
