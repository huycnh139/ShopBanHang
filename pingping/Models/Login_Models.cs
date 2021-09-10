using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pingping.Models
{
    public class Login_Models
    {
        [MaxLength(150)]
        [Required(ErrorMessage = "Sai tài khoản hoặc mật khẩu.")]
        public string username  { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Sai tài khoản hoặc mật khẩu.")]
        public string password { get; set; }

        public bool checkbox { get; set; }

    }
}