namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NguoiBan")]
    public partial class NguoiBan
    {
        [Key]
        public int id_nguoiban { get; set; }

        public int? id_taikhoan { get; set; }

        [StringLength(100)]
        public string taikhoanng { get; set; }

        [StringLength(100)]
        public string nganhang { get; set; }

        public int phone { get; set; }

        [StringLength(100)]
        public string street { get; set; }

        [StringLength(100)]
        public string ward { get; set; }

        [StringLength(100)]
        public string district { get; set; }

        [StringLength(100)]
        public string province { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
