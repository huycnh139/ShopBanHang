using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class HoaDon_DAO
    {
        Context_ db = null;
        public HoaDon_DAO()
        {
            db = new Context_();
        }
        
        public double? get_hoadon_doanhthu()
        {
            db.Configuration.ProxyCreationEnabled = false;

            double? total= 0;
            var res_= db.HoaDons.Where(x => x.trangthai == "Đã Thanh Toán").ToList();
            if (res_ != null)
            {
                foreach (var i in res_)
                {
                    total += i.tonggia;
                }
            }
            return total;
        }
        public int get_count()
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.Count();
        }
        
        public int get_count_trangthai(string trangthai)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.Count(x=>x.trangthai==trangthai);
        }

        public HoaDon get_hoadon_id(int id_hoadon)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
        }

        public HoaDon get_hoadon_trangthai(int id_nguoimua,string status)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.FirstOrDefault(x => x.id_nguoimua == id_nguoimua&&x.trangthai== status);
        }
        public HoaDon get_hoadon_trangthai_(int id_nguoimua, string status)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.FirstOrDefault(x => x.id_nguoimua == id_nguoimua && x.trangthai == status && x.hinhthuctt == "Mới Đặt");
        }

        // nếu hóa đơn nào chưa đc thanh toán thì lấy ra
        public int get_hoadon_trangthai(int id_nguoimua)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res= db.HoaDons.FirstOrDefault(x => x.id_nguoimua == id_nguoimua && x.trangthai == "Chưa Thanh Toán");

            if (res == null)
            {
                return 0;
            }
            return res.id_hoadon;
        }

        public int updateHoaDon_quantity(int id_hoadon,int soluong,double? gia)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var dao = new HoaDonCT();
            int quantity = db.HoaDonCTs.Count(x => x.id_hoadon == id_hoadon);
            var res = db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
            if (res!=null){
                res.soluong = quantity;
                res.tonggia = gia;
                db.SaveChanges();

                return 1;
            }
            return 0;
        }
        
        public int update_hoadon_maghn(int? id_hoadon, string maghn)
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var dao = new HoaDonCT_DAO();
            var res = db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
            if (res != null)
            {
                res.duyet = true;
                res.maghn = maghn;
                res.trangthaigh = "Đang Đóng Gói";
                db.SaveChanges();

                var res_hdct = db.HoaDonCTs.Where(x => x.id_hoadon == id_hoadon).ToList();
                foreach (var c in res_hdct)
                {
                    c.trangthai = "Đã Thanh Toán";
                    db.SaveChanges();
                }

                return 1;
            }
            return 0;
        }
        public HoaDon createHoaDon(int id_nguoimua)
        {
            db.Configuration.ProxyCreationEnabled = false;

            HoaDon result = new HoaDon();
            DateTime now = DateTime.Now;

            var modal_To_EF = new HoaDon()
            {
                id_nguoimua=id_nguoimua,
                //mahd
                //tonggia
                thoigian=now,
                hinhthuctt ="Mới Đặt",
                freeship=0,
                trangthai= "Chưa Thanh Toán"
            };

            result = db.HoaDons.Add(modal_To_EF);
            db.SaveChanges();
            if (result != null) return result;
            return null;
        }

        public List<HoaDon> get_hoadon_taikhoan_id(int id_taikhoan)
        {
            db.Configuration.ProxyCreationEnabled = false;

            return db.HoaDons.Where(x => x.id_nguoimua == id_taikhoan).ToList();
        }

        #region 13-01
        public List<HoaDon> get_hoadon_damua_all()
        {
            return db.HoaDons.Where(n => n.trangthai == "Đã Thanh Toán").OrderBy(n => n.thoigian).ToList();
        }
        #endregion

        public int updateHoaDon_coupon(int id_hoadon,double? value)
        {
            db.Configuration.ProxyCreationEnabled = false;
            
            var res = db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
            if (res != null)
            {
                res.tonggia -= value;
                db.SaveChanges();

                return 1;
            }
            return 0;
        }

        public int update_hoadon(HoaDon hd)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var res = db.HoaDons.FirstOrDefault(x => x.id_hoadon == hd.id_hoadon);
            if (res != null)
            {
                res.freeship = hd.freeship;
                res.hinhthuctt = hd.hinhthuctt;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public HoaDon get_hoadon_NguoiMua(int id_nguoimua)
        {
            return db.HoaDons.FirstOrDefault(n => n.id_nguoimua == id_nguoimua && n.trangthai == "Chưa Thanh Toán");
        }
        public HoaDon set_hd_paypal(int id_nguoimua, double sotiendathanhtoan, string magiaodich)
        {
            var res = db.HoaDons.FirstOrDefault(n => n.id_nguoimua == id_nguoimua && n.trangthai == "Chưa Thanh Toán");
            res.hinhthuctt = "PayPal";
            res.trangthai = "Đã Thanh Toán";
            double a = 22950;
            DateTime now = DateTime.Now;
            res.thoigian = now;
            res.sotiendathanhtoan = sotiendathanhtoan * a;
            res.magiaodich = magiaodich;
            db.SaveChanges();
            return res;
        }
        public List<HoaDon> get_hoadon_taikhoan_dtt(int id_taikhoan)
        {
            return db.HoaDons.Where(x => x.id_nguoimua == id_taikhoan && x.trangthai == "Đã Thanh Toán").ToList();
        }
        public HoaDon get_hoadon_idd(int id_hoadon)
        {
            
            return db.HoaDons.FirstOrDefault(x => x.id_hoadon == id_hoadon);
        }
    }
}
