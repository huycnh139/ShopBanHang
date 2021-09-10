using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class MyOrder_Model
    {
        public List<HoaDon> hoadon_ { get; set; }
        public List<HoaDonCT> hoadonct_ { get; set; }
        public List<SanPham> sanpham_ { get; set; }
        public List<Size> size_ { get; set; }
        public List<Color> color_ { get; set; }
    }
}