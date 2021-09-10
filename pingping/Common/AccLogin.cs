using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Common
{
    //lưu thông tin cần nhớ tạm  lên ổ đĩa (session, ...)
    [Serializable]
    public class AccLogin
    {
        public int id_taikhoan { get; set; }
        public int id_nguoi { get; set; }

        public int phone { get; set; }
        public string street { get; set; }
        public string ward { get; set; }
        public string dictrict { get; set; }
        public string province { get; set; }
        public string taikhoanng { get; set; }
        public string nganhang { get; set; }
        public string username { get; set; }
        public string password_old { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool loaitk { get; set; }
        public string hoten { get; set; }
    }
}