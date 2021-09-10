using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class DauGia_SanPham_LichSu_Model
    {
        public List<DauGia> daugia_ { get; set; }
        public List<SanPham> sanpham_ { get; set; }
        public List<LichSuDG> lichsudg_ { get; set; }
    }
}