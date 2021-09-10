using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class Account_buyer_Model
    {
        public int? id_taikhoan { get; set; }
        public string email { get; set; }

        public bool loaitk { get; set; }

        [StringLength(150)]
        public string hoten { get; set; }
        public int phone { get; set; }

        [StringLength(100)]
        public string street { get; set; }
            
        [StringLength(100)]
        public string ward { get; set; }

        [StringLength(100)]
        public string district { get; set; }

        [StringLength(100)]
        public string province { get; set; }
        [StringLength(250)]
        public string username { get; set; }

        [StringLength(250)]
        public string password { get; set; }
    }
}
