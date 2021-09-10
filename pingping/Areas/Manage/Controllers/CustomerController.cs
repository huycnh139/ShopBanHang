using Modal.DAO;
using Modal.EF;
using pingping.Common;
using pingping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace pingping.Areas.Manage.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Manage/Customer
        public ActionResult QuanLyKhachHang()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }
            else if (session_acc.loaitk)
                return RedirectToAction("../../Home/Index");
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";
            ViewBag.HoTen = session_acc.hoten;

            var dao = new NguoiMua_DAO();
            var dao1 = new TaiKhoan_DAO();
            var lstNguoiMua = dao.get_nguoimua_all();
            List<Account_buyer_Model> tkngmua = new List<Account_buyer_Model>();
            TaiKhoan tk = new TaiKhoan();
            foreach (var item in lstNguoiMua)
            {
                tk = dao1.Get_id_taikhoanAdmin(item.id_taikhoan);
                tkngmua.Add(new Account_buyer_Model
                {
                    id_taikhoan = item.id_taikhoan,
                    hoten = tk.hoten,
                    email = tk.email,
                    loaitk = tk.loaitk,
                    phone = item.phone,
                    street = item.street,
                    ward = item.ward,
                    district = item.district,
                    province = item.province
                });
            }
            return View(tkngmua);
        }
        public ActionResult DeleteAccount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new TaiKhoan_DAO();
            var res = dao.remove_account_idtknm(id);
            if (res == 1)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Xoá Thành Công";
                
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Xoá Thất Bại!";
            }
            var dao1 = new NguoiMua_DAO();
            var lstNguoiMua = dao1.get_nguoimua_all();
            List<Account_buyer_Model> tkngmua = new List<Account_buyer_Model>();
            TaiKhoan tk = new TaiKhoan();
            foreach (var item in lstNguoiMua)
            {
                tk = dao.Get_id_taikhoanAdmin(item.id_taikhoan);
                tkngmua.Add(new Account_buyer_Model
                {
                    id_taikhoan = item.id_taikhoan,
                    hoten = tk.hoten,
                    email = tk.email,
                    loaitk = tk.loaitk,
                    phone = item.phone,
                    street = item.street,
                    ward = item.ward,
                    district = item.district,
                    province = item.province
                });
            }
            return View("QuanLyKhachHang", tkngmua);
        }
        //public ActionResult CreateBuyer(Account_buyer_Model acc)
        //{
        //    if (acc == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var dao = new TaiKhoan_DAO();
        //    TaiKhoan res = dao.create_taikhoan(acc.username, CreateMD5(acc.password), acc.hoten, acc.email);

        //    if (res != null)
        //    {
        //        var id_taikhoan = res.id_taikhoan;
        //        var dao1 = new NguoiMua_DAO();
        //        NguoiMua res1 = dao1.create_nguoimua_Admin(id_taikhoan, acc.phone, acc.street, acc.ward, acc.district, acc.province);
        //        if (res1 != null)
        //        {
        //            ViewBag.notify = 1;
        //            ViewBag.action = "Xoá Thành Công";
        //        }
        //        else
        //        {
        //            ViewBag.notify = "Thêm thành viên thât bại";
        //            ViewBag.Alert = "alert-danger";
        //        }
        //    }
        //    var dao2 = new NguoiMua_DAO();
        //    var lstNguoiMua = dao2.get_nguoimua_all();
        //    List<Account_buyer_Model> tkngmua = new List<Account_buyer_Model>();
        //    TaiKhoan tk = new TaiKhoan();
        //    foreach (var item in lstNguoiMua)
        //    {
        //        tk = dao.Get_id_taikhoanAdmin(item.id_taikhoan);
        //        tkngmua.Add(new Account_buyer_Model
        //        {
        //            id_taikhoan = item.id_taikhoan,
        //            hoten = tk.hoten,
        //            email = tk.email,
        //            loaitk = tk.loaitk,
        //            phone = item.phone,
        //            street = item.street,
        //            ward = item.ward,
        //            district = item.district,
        //            province = item.province
        //        });
        //    }
        //    return RedirectToAction("QuanLyKhachHang", tkngmua);
        //}
        public ActionResult ResetPassword(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StringBuilder sb = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                sb.Append(c);
            }
            var dao = new NguoiMua_DAO();
            var res = dao.get_infor_(id);
            var content = "<div style=" + "color:#FFCC00" + ">Hệ Thống PingPing Chào Bạn!</div></br><div style=" + "font-size:20px" + ">Mật Khẩu của bạn là: <strong style=" + "color:#0000FF" + ">" + sb + "</strong></div>";
            GuiEmail("Reset Password PingPing", res.TaiKhoan.email.ToString(), "testecommercehcmue@gmail.com", "Taogmail.com", content.ToString());
            var dao1 = new TaiKhoan_DAO();
            var res2 =dao1.update_pass(res.TaiKhoan.id_taikhoan, CreateMD5(sb.ToString()));
            if (res2 != null)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Thay Đổi Mật Khẩu Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Thay Đổi Mật Khẩu Thất Bại";
            }
            
            var lstNguoiMua = dao.get_nguoimua_all();
            List<Account_buyer_Model> tkngmua = new List<Account_buyer_Model>();
            TaiKhoan tk = new TaiKhoan();
            foreach (var item in lstNguoiMua)
            {
                tk = dao1.Get_id_taikhoanAdmin(item.id_taikhoan);
                tkngmua.Add(new Account_buyer_Model
                {
                    id_taikhoan = item.id_taikhoan,
                    hoten = tk.hoten,
                    email = tk.email,
                    loaitk = tk.loaitk,
                    phone = item.phone,
                    street = item.street,
                    ward = item.ward,
                    district = item.district,
                    province = item.province
                });
            }
            return View("QuanLyKhachHang", tkngmua);
        }
        public void GuiEmail(string Title, string ToEmail, string FromEmail, string Password, string Content)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(ToEmail);// Địa Chỉ Người Nhận 
            mail.From = new MailAddress(ToEmail); // Địa Chỉ Người Gửi
            mail.Subject = Title;// Tieu Đê
            mail.Body = Content;// Noi Dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";// host gui Email
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmail, Password);//Tai Khoan Nguoi Gui
            smtp.EnableSsl = true;// kich hoat giao tiep an toan SSL
            smtp.Send(mail);//Gui mail di

        }
        public ActionResult UpdateAdmin(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new TaiKhoan_DAO();
            var dao1 = new NguoiMua_DAO();
            var res = dao1.get_infor_(id);
            var a= dao.update_admin(res.TaiKhoan.id_taikhoan);

            if (a != null)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Nâng Cấp Tài Khoản Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Nâng Cấp Tài Khoản Thất Bại";
            }

            var lstNguoiMua = dao1.get_nguoimua_all();
            List<Account_buyer_Model> tkngmua = new List<Account_buyer_Model>();
            TaiKhoan tk = new TaiKhoan();
            foreach (var item in lstNguoiMua)
            {
                tk = dao.Get_id_taikhoanAdmin(item.id_taikhoan);
                tkngmua.Add(new Account_buyer_Model
                {
                    id_taikhoan = item.id_taikhoan,
                    hoten = tk.hoten,
                    email = tk.email,
                    loaitk = tk.loaitk,
                    phone = item.phone,
                    street = item.street,
                    ward = item.ward,
                    district = item.district,
                    province = item.province
                });
            }
            return View("QuanLyKhachHang", tkngmua);
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}