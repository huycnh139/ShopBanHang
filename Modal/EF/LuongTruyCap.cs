namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuongTruyCap")]
    public partial class LuongTruyCap
    {
        [Key]
        public int id_ltc { get; set; }

        public int soluong_vl { get; set; }
        public string namepage { get; set; }
        public int soluong_kh { get; set; }
        public DateTime? time_update { get; set; }
    }
}
