using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class SanPham_DAO
    {
        Context_ db = null;
        public SanPham_DAO()
        {
            db = new Context_();
        }
        
        public List<SanPham> get_product(string type)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.Where(x=>x.xeploai==type).ToList();
        }
        public List<SanPham> get_product_idloaisp(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.Where(x => x.id_loaisp == id).ToList();
        }

        public SanPham get_product_idsanpham(int? id_sanpham)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.FirstOrDefault(x => x.id_sanpham == id_sanpham);
        }
        public List<SanPham> get_product_idsanpham_(int? id_sanpham)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.Where(x => x.id_sanpham == id_sanpham).ToList();
        }

        public List<SanPham> get_product_all()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.ToList();
        }
        public SanPham get_product_(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.FirstOrDefault(x => x.id_sanpham == id);
        }
        public SanPham get_product_id(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.FirstOrDefault(x => x.id_sanpham == id);
        }
        public int get_count()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.Count(x=>x.hienthi=="HIỆN");
        }
        public int get_count_hide()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.Count(x => x.hienthi == "ẨN");
        }

        public List<SanPham> get_product_all_()
        {
            //db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.ToList();
        }
        public SanPham get_product__(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.SanPhams.FirstOrDefault(x => x.id_sanpham == id);
        }
        public SanPham set_product(SanPham type)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (type != null)
            {
                db.SanPhams.Add(type);
                db.SaveChanges();
                return type;
            }
            return null;
        }
        public SanPham remove_product(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var type = db.SanPhams.SingleOrDefault(x => x.id_sanpham == id);
            if (type != null)
            {
                type.hienthi = "ẨN";
                db.SaveChanges();
                return type;
            }
            return null;
        }
        public SanPham update_product(int? id_sanpham, string tensp, int id_loaisp, string tenngan, int soluong, double dongia, double giasale, string trangthai, string hienthi, string tinhtrang, string thongtin, string XepLoai,string hinhanh1,string hinhanh2,string hinhanh3,string hinhanh4)
        {
            var type = db.SanPhams.SingleOrDefault(x => x.id_sanpham == id_sanpham);
            if (type != null)
            {
                type.hinhanh1 = hinhanh1;
                type.hinhanh2 = hinhanh2;
                type.hinhanh3 = hinhanh3;
                type.hinhanh4 = hinhanh4;
                type.tensp = tensp;
                type.tensp = tensp;
                type.id_loaisp = id_loaisp;
                type.soluong = soluong;
                type.dongia = dongia;
                type.giasale = giasale;
                type.trangthai = trangthai;
                type.hienthi = hienthi;
                type.tinhtrang = tinhtrang;
                type.thongtin = thongtin;
                type.xeploai = XepLoai;
                db.SaveChanges();
                return type;
            }
            return null;
        }
        public SanPham update_product_model(SanPham sp)
        {
            SanPham sanpham = new SanPham();
            sanpham = sp;
            db.SaveChanges();
            return null;
        }

        public IQueryable<SanPham> get_search_product(string sTuKhoa)
        {
            if (sTuKhoa == null)
            {
                return db.SanPhams.OrderBy(n => n.tensp);
            }
            return db.SanPhams.Where(n => n.tensp.Contains(sTuKhoa)).OrderBy(n => n.tensp);
        }
        public IQueryable<SanPham> get_search_category(string sTuKhoa, string searchselect)
        {
            if (sTuKhoa == null)
            {
                return db.SanPhams.Where(n => n.LoaiSanPham.theloai == searchselect).OrderBy(n => n.tensp);
            }
            return db.SanPhams.Where(n => n.tensp.Contains(sTuKhoa) && n.LoaiSanPham.theloai == searchselect).OrderBy(n => n.tensp);
        }
        public List<SanPham> get_product_category()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SanPhams.Where(x => x.LoaiSanPham.theloai == "Áo").ToList();
        }
        public List<SanPham> get_product_all_status_show()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.SanPhams.Where(x => x.hienthi == "HIỆN").ToList();
        }

        public IQueryable<SanPham> get_search_product_(string sTuKhoa)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (sTuKhoa == null)
            {
                return db.SanPhams.OrderBy(n => n.tensp);
            }
            return db.SanPhams.Where(n => n.tensp.Contains(sTuKhoa)).OrderBy(n => n.tensp);
        }
    }
}
