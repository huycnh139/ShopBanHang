using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class NguoiBan_DAO
    {
        Context_ db = null;
        public NguoiBan_DAO()
        {
            db = new Context_();
        }

        public NguoiBan get_infor(int id_taikhoan)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.NguoiBans.FirstOrDefault(x => x.id_taikhoan == id_taikhoan);
        }
    }
}
