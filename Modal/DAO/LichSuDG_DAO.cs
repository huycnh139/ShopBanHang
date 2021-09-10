using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class LichSuDG_DAO
    {
        Context_ db = null;
        public LichSuDG_DAO()
        {
            db = new Context_();
        }

        public List<LichSuDG> get_lichsudg()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.LichSuDGs.ToList();
        }
        public List<LichSuDG> get_lichsudg_daugia_id(int id_daugia)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.LichSuDGs.Where(x => x.id_daugia == id_daugia).ToList();
        }

        public List<LichSuDG> get_lichsudg_daugia_taikhoan_id(int? id_daugia,int? id_taikhoan)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.LichSuDGs.Where(x => x.id_daugia == id_daugia&&x.id_taikhoan==id_taikhoan).ToList();
        }

        public int create_lichsudg_id_taikhoan_daugia(int id_daugia,int id_taikhoan,float value)
        {
            DateTime now = DateTime.Now;
            db.Configuration.ProxyCreationEnabled = false;

            var modal_To_EF = new LichSuDG()
            {
                id_taikhoan=id_taikhoan,
                id_daugia=id_daugia,
                value=value,
                time_update=now
            };

            var result = db.LichSuDGs.Add(modal_To_EF);
            db.SaveChanges();
            if (result != null) return result.id_lichsudg;
            return 0;
        }

        public List<LichSuDG> get_lsdg(int id_daugia)
        {
            return db.LichSuDGs.Where(x => x.id_daugia == id_daugia).ToList();
        }
    }
}
