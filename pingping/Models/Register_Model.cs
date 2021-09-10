using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class Register_Model
    {
        public string username { get; set; }
        public string password { get; set; }
        public string hoten { get; set; }
        public string email { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số Điện Thoại Không Đúng")]
        public int phone { get; set; }
    }
}