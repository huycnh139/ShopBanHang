using Modal.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modal.DAO
{
    public class TaiKhoan_DAO
    {
        Context_ db = null;
        public TaiKhoan_DAO()
        {
            db = new Context_();
        }

        public int get_count()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.TaiKhoans.Count();
        }
        public int get_count_loai(bool loaitk)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.TaiKhoans.Count(x=>x.loaitk==loaitk);
        }

        public TaiKhoan Get_id_taikhoan(string user, string pass)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.TaiKhoans.FirstOrDefault(x => x.username == user && x.password == pass);
            return res;
        }
        public TaiKhoan create_taikhoan(string username, string password, string hoten, string email)
        {
            db.Configuration.ProxyCreationEnabled = false;
            TaiKhoan tk_ =new TaiKhoan();
            var tk = new TaiKhoan()
            {
                username=username,
                password=password,
                password_old=password,
                hoten=hoten,
                email=email,
                loaitk=true,
            };

            tk_ = db.TaiKhoans.Add(tk);
            db.SaveChanges();
            if (tk_ != null) return tk_;
            return null;
        }
        public int CheckLogin(string user, string pass)
        {
            db.Configuration.ProxyCreationEnabled = false;

            if (db.TaiKhoans.Count(x => x.username == user) > 0)
            {
                if (db.TaiKhoans.Count(x => x.username == user && x.password == pass)> 0)
                {
                    if (db.TaiKhoans.Count(x => x.username == user && x.loaitk == false) > 0) 
                    {
                        return 2; //saller
                    }
                    else return 1;  //buyer
                }
                else return -1;
            }
            else
            {
                return 0;
            }
        }
        public TaiKhoan update_account(string email, string hoten, int id_taikhoan)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.TaiKhoans.FirstOrDefault(x => x.id_taikhoan == id_taikhoan);
            if (res != null)
            {
                res.hoten = hoten;
                res.email = email;
                db.SaveChanges();
                return res;
            }
            return null;
        }
        public TaiKhoan update_pass(int id_taikhoan, string pass)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.TaiKhoans.FirstOrDefault(x => x.id_taikhoan == id_taikhoan);
            if (res != null)
            {
                res.password_old = res.password;
                res.password = pass;
                db.SaveChanges();
                return res;
            }
            return null;
        }

        public int remove_account_idtknm(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.TaiKhoans.FirstOrDefault(x => x.id_taikhoan == id);
            var res1 = db.NguoiMuas.FirstOrDefault(x => x.id_taikhoan == id);
            if (res != null && res1 != null)
            {
                db.TaiKhoans.Remove(res);
                db.NguoiMuas.Remove(res1);
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public TaiKhoan Get_id_taikhoanAdmin(int? id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var res = db.TaiKhoans.FirstOrDefault(x => x.id_taikhoan == id);
            return res;
        }

        #region 15/01
        public TaiKhoan forgotpassword(string email, string username, string mk)
        {
            var res = db.TaiKhoans.FirstOrDefault(x => x.username == username && x.email == email);
            if (res != null)
            {
                res.password_old = res.password;
                res.password = mk;
                db.SaveChanges();
                return res;
            }
            return null;
        }
        public TaiKhoan update_admin(int? id)
        {
            var res = db.TaiKhoans.FirstOrDefault(x => x.id_taikhoan == id);
            if (res != null)
            {
                res.loaitk = false;
                var res1 = db.NguoiMuas.FirstOrDefault(x => x.id_taikhoan == res.id_taikhoan);
                NguoiBan nm = new NguoiBan();
                nm.id_taikhoan = res1.id_taikhoan;
                nm.phone = res1.phone;
                nm.street = res1.street;
                nm.ward = res1.ward;
                nm.district = res1.district;
                nm.province = res1.province;
                db.NguoiBans.Add(nm);
                db.NguoiMuas.Remove(res1);
                db.SaveChanges();
                return res;
            }
            return null;
        }
        #endregion
    }
}
