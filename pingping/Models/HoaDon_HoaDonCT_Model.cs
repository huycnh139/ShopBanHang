using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class HoaDon_HoaDonCT_Model
    {
        public int id_hoadonct { get; set; }

        public int? id_hoadon { get; set; }

        public int? id_sanpham { get; set; }

        public int? id_nguoimua { get; set; }

        public double? dongia { get; set; }

        public DateTime thoigian { get; set; }

        public int? soluong { get; set; }

        public string trangthaihd { get; set; }

        public string tensp { get; set; }

        public string tenngan { get; set; }

        //public double giasale { get; set; } = dongia

        public string trangthaisp { get; set; }

        public string barcode { get; set; }

        public string tinhtrangsp { get; set; }

        public string thongtin { get; set; }

        public string hinhanh1 { get; set; }

        public string hinhanh2 { get; set; }

        public string hinhanh3 { get; set; }

        public string hinhanh4 { get; set; }

        public string size { get; set; }

        public string xeploai { get; set; }

        public double? tonggiact { get; set; }
    }
}