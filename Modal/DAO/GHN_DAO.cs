using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class GHN_DAO
    {
        Context_ db = null;
        public GHN_DAO()
        {
            db = new Context_();
        }

        public int create_ghn(GHN_Ship ghn)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var a = db.GHN_Ship.Add(ghn);
            if (a != null)
            {
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public GHN_Ship get_dhn_hoadon_id(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res_ghn = db.GHN_Ship.FirstOrDefault(x => x.id_hoadon==id);
            return res_ghn;
        }
        public GHN_Ship get_ghn_id(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res_ghn = db.GHN_Ship.FirstOrDefault(x => x.id_ship == id);
            return res_ghn;
        }
    }
}
