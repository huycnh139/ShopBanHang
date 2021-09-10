namespace Modal.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coupon")]
    public partial class Coupon
    {
        [Key]
        public int id_coupon { get; set; }
        public int id_sanpham { get; set; }
        public string discount { get; set; }
        public string status_ { get; set; }
        public string Ma_Coupon { get; set; }
        public string thestart { get; set; }
        public string theend { get; set; }
    }
}