using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class SanPham_Size_Model
    {
        public SanPham SanPham_ { get; set; }
        public List<Size> Size_ { get; set; }
    }
}