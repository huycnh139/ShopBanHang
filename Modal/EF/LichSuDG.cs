namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuDG")]
    public partial class LichSuDG
    {
        [Key]
        public int id_lichsudg { get; set; }

        public int? id_taikhoan { get; set; }

        public int? id_daugia { get; set; }

        public double? value { get; set; }

        public DateTime? time_update { get; set; }

        public virtual DauGia DauGia { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
