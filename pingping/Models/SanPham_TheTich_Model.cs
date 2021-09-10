using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class SanPham_TheTich_Model
    {
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

        [StringLength(10)]
        public string tinhtrang { get; set; }

        public string thongtin { get; set; }

        public string hinhanh1 { get; set; }

        public string hinhanh2 { get; set; }

        public string hinhanh3 { get; set; }

        public string hinhanh4 { get; set; }

        [StringLength(100)]
        public string xeploai { get; set; }
        public double? chieucao { get; set; }

        public double? chieurong { get; set; }

        public double? chieudai { get; set; }

        public double? cannang { get; set; }
    }
}