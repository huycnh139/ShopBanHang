using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class TheTich_DAO
    {
        Context_ db = null;
        public TheTich_DAO()
        {
            db = new Context_();
        }
        public int set_vol(TheTich type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (type != null)
            {
                db.TheTiches.Add(type);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public TheTich get_vol(int id)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            if (id != 0)
            {
                return db.TheTiches.FirstOrDefault(x => x.id_sanpham == id);
            }
            return null;
        }
        public TheTich get_vol_(int? id)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            if (id != 0)
            {
                return db.TheTiches.FirstOrDefault(x => x.id_sanpham == id);
            }
            return null;
        }
        public TheTich update_vol(int id_sanpham, double chieucao, double chieurong, double chieudai, double cannang)
        {
            var type = db.TheTiches.SingleOrDefault(x => x.id_sanpham == id_sanpham);
            if (type != null)
            {
                type.chieucao = chieucao;
                type.chieurong = chieurong;
                type.chieudai = chieudai;
                type.cannang = cannang;
                db.SaveChanges();
                return type;
            }
            return null;
        }
    }
}