using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Areas.Manage.Models
{
    public class Color_Size
    {
        public int? id_sanpham { get; set; }
        public int? id_color { get; set; }
        public string tensp { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public int? soluong { get; set; }

        public int? soluong_sp { get; set; }

        public int? soluong_size { get; set; }
    }
}