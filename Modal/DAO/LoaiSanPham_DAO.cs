using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class LoaiSanPham_DAO
    {
        Context_ db = null;
        public LoaiSanPham_DAO()
        {
            db = new Context_();
        }

        public List<LoaiSanPham> get_category_all()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.LoaiSanPhams.ToList();
        }

        public int get_category_shortname(string type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.LoaiSanPhams.FirstOrDefault(x => x.tenngan == type);
            return res.id_loaisp;
        }

        public int remove_category_idloaisanpham(int? type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.LoaiSanPhams.FirstOrDefault(x => x.id_loaisp == type);
            if (res != null)
            {
                db.LoaiSanPhams.Remove(res);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int set_category(LoaiSanPham type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (type != null)
            {
                db.LoaiSanPhams.Add(type);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }
        public LoaiSanPham gett_category_id(int? idloaisp)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.LoaiSanPhams.SingleOrDefault(x => x.id_loaisp == idloaisp);
            if (res != null)
            {
                return res;
            }
            return null;
        }

        public int update_category_id(LoaiSanPham model)
        {
            LoaiSanPham res = db.LoaiSanPhams.SingleOrDefault(x => x.id_loaisp == model.id_loaisp);
            res.tenloai = model.tenloai;
            res.tenngan = model.tenngan;
            res.theloai = model.theloai;
            res.hinhanh = model.hinhanh;
            res.xeploai = model.xeploai;
            res.thongtin = model.thongtin;
            db.SaveChanges();
            return 0;
        }


        public List<LoaiSanPham> get_category_id(int idloaisp)
        {
            db.Configuration.ProxyCreationEnabled = false;


            return db.LoaiSanPhams.Where(x => x.id_loaisp == idloaisp).ToList();

        }

        public List<LoaiSanPham> get_category_all_()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            return db.LoaiSanPhams.ToList();
        }

    }
}
