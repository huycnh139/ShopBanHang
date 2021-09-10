namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Size")]
    public partial class Size
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Size()
        {
            Colors = new HashSet<Color>();
        }

        [Key]
        public int id_size { get; set; }

        public int? id_sanpham { get; set; }

        [Column("size")]
        [StringLength(5)]
        public string size { get; set; }

        public int? soluong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Color> Colors { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
