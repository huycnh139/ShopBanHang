using Modal.DAO;
using Modal.EF;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using pingping.Areas.Manage.Models;
using pingping.Common;
using pingping.Models;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace pingping.Areas.Manage.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage/Home
        #region ThongKe
        public ActionResult Index()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }
            else if (session_acc.loaitk)
                return RedirectToAction("../../Home/Index");
            else
            {
                ViewBag.Name = session_acc.hoten;
                ViewBag.Messager = "Đăng Xuất";
                ViewBag.Login = "../Accounts/Logout";
                var daosp = new SanPham_DAO();

                var dao = new LuongTruyCap_DAO();
                var res_ = dao.get_luongtruycap();
                ViewBag.listLuongTruyCap=res_;
                ViewBag.lstsp = daosp.get_product_all_status_show();
                ViewBag.HoTen = session_acc.hoten;
                return View();
            }
        }
        
        public ActionResult Tables()
        {
            return View();
        }
        public void DownloadExcel(List<int> arr,DateTime fromdate,DateTime todate)
        {
            var dao = new HoaDonCT_DAO();
            var lst = dao.get_hoadon_damua_all_exportexcel(arr,fromdate,todate);
            
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1:J1"].Merge = true;
            Sheet.Cells["A1:J1"].Value = "Report";
            Sheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            Sheet.Cells["A1:J1"].Style.Font.Size = 18;
            Sheet.Cells["A2"].Value = "No";
            Sheet.Cells["B2"].Value = "Name";
            Sheet.Cells["C2"].Value = "Number Phone";
            Sheet.Cells["D2"].Value = "Order Date";
            Sheet.Cells["E2"].Value = "Product";
            Sheet.Cells["F2"].Value = "Size";
            Sheet.Cells["G2"].Value = "Color";
            Sheet.Cells["H2"].Value = "Quantity";
            Sheet.Cells["I2"].Value = "Price";
            Sheet.Cells["J2"].Value = "Total Amount";
            int row = 3;
            foreach (var item in lst)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = row - 2;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.HoaDon.NguoiMua.TaiKhoan.hoten;
                Sheet.Cells[string.Format("C{0}", row)].Value = String.Format("0{0}",item.HoaDon.NguoiMua.phone);
                Sheet.Cells[string.Format("D{0}", row)].Value = String.Format("{0:d/M/yyyy HH:mm:ss}", item.HoaDon.thoigian);
                Sheet.Cells[string.Format("E{0}", row)].Value = item.SanPham.tensp;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.id_size;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.color;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.soluong;

                var info = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");
                Sheet.Cells[string.Format("I{0}", row)].Value = String.Format(info, "{0:c}", item.dongia);
                Sheet.Cells[string.Format("J{0}", row)].Value = String.Format(info, "{0:c}", item.soluong* item.dongia);
                row++;
            }

            var modelRows = row - 1;
            string modelRange = "A1:J" + modelRows.ToString();
            var modelTable = Sheet.Cells[modelRange];

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        [ChildActionOnly]
        public ActionResult Chart_Line()
        {
            //Statistis_Model statistis = new Statistis_Model();

            var dao_sp = new SanPham_DAO();
            var dao_hd = new HoaDon_DAO();
            var dao_mb = new TaiKhoan_DAO();
            var dao_dg = new DauGia_DAO();

            int sp = dao_sp.get_count();
            int sphide = dao_sp.get_count_hide();

            int hd = dao_hd.get_count();
            int hdtt = dao_hd.get_count_trangthai("Đã Thanh Toán");

            int member = dao_mb.get_count();
            int mbad = dao_mb.get_count_loai(false);
            int mbkh = dao_mb.get_count_loai(true);

            int dg = dao_dg.get_count();
            int dgrun = dao_dg.get_count_stop();
            int dgstop = dao_dg.get_count_run();
            double? total = dao_hd.get_hoadon_doanhthu();
            //statistis = new Statistis_Model()
            //{
            //    sp = sp,
            //    sphide = sphide,
            //    hd = hd,
            //    mb = member,
            //    mbad = mbad,
            //    mbkh = mbkh,
            //    dg = dg,
            //    dgrun = dgrun,
            //    dgstop = dgstop
            //};
            ViewBag.hdtt = hdtt;
            ViewBag.total = total;
            ViewBag.sp = sp;
            ViewBag.hd = hd;
            ViewBag.kh = mbkh;
            ViewBag.dg = dg;
            //ViewBag.statistis = statistis;
            return PartialView("Chart_Line");
        }

        public JsonResult Count_statistis()
        {
            Object statistis = new Object();

            var dao_sp = new SanPham_DAO();
            var dao_hd = new HoaDon_DAO();
            var dao_mb = new TaiKhoan_DAO();
            var dao_dg = new DauGia_DAO();

            int sp = dao_sp.get_count();
            int sphide = dao_sp.get_count_hide();

            int hd= dao_hd.get_count();
            int hdtt = dao_hd.get_count_trangthai("Đã Thanh Toán");

            int member = dao_mb.get_count();
            int mbad = dao_mb.get_count_loai(false);
            int mbkh = dao_mb.get_count_loai(true);

            int dg = dao_dg.get_count();
            int dgrun = dao_dg.get_count_run();
            int dgstop = dao_dg.get_count_stop();

            statistis = new
            {
                sp = sp,
                sphide=sphide,
                hd = hd,
                mb = member,
                mbad=mbad,
                mbkh=mbkh,
                dg = dg,
                dgrun=dgrun,
                dgstop=dgstop,
                hdtt= hdtt
            };
            if (statistis != null)
            {
                return Json(statistis, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LoaiSanPham
        public ActionResult QuanLyLoaiSanPham()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }

            ViewBag.Name = session_acc.hoten;
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";

            var dao = new LoaiSanPham_DAO();
            var lstLoaiSanPham = dao.get_category_all();
            return View(lstLoaiSanPham);
        }
        public ActionResult DeleteLoaiSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new LoaiSanPham_DAO();
            var res = dao.remove_category_idloaisanpham(id);
            if (res == 1)
            {
                ViewBag.notify = "Xoá Thành Công";
                ViewBag.Alert = "alert-info";
            }
            else
            {
                ViewBag.notify = "Xoá Thất Bại!";
                ViewBag.Alert = "alert-danger";
            }
            var lstLoaiSanPham = dao.get_category_all();
            return View("QuanLyLoaiSanPham", lstLoaiSanPham);
        }
        public ActionResult CreateLoaiSP(LoaiSanPham lsp, HttpPostedFileBase hinhanh)
        {
            if (lsp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (hinhanh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new LoaiSanPham_DAO();
            //Kiem Tra hình tồn tại trong csdl chưa
            if (hinhanh.ContentLength > 0)
            {
                if (hinhanh.ContentType != "image/jpeg" && hinhanh.ContentType != "image/png" && hinhanh.ContentType != "image/gif" && hinhanh.ContentType != "image/tiff" && hinhanh.ContentType != "image/BMP" && hinhanh.ContentType != "image/jpg")
                {
                    ViewBag.upload = "không phải định dạng của hình ảnh";
                    var lstcategory = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstcategory);
                }
                //Lay hinh anh
                var fileName = Path.GetFileName(hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Source/img-public"), fileName);
                //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Hình đã tồn tại";
                    var lstcategory = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstcategory);
                }
                else
                {
                    //Lấy Hình Ảnh đưa vào thư muc HinhAnhSP
                    hinhanh.SaveAs(path);
                    lsp.hinhanh = fileName;

                    var res = dao.set_category(lsp);
                    if (res == 1)
                    {
                        ViewBag.notify = "Thêm loại sản phẩm thành công";
                        ViewBag.Alert = "alert-info";
                    }
                    else
                    {
                        ViewBag.notify = "Thêm loại sản phẩm thât bại";
                        ViewBag.Alert = "alert-danger";
                    }
                }
            }
            var lstLoaiSanPham = dao.get_category_all();
            return View("QuanLyLoaiSanPham", lstLoaiSanPham);
        }

        [HttpGet]
        public JsonResult Get_LoaiSanPham_id(int id_loaisp)
        {
            var dao = new LoaiSanPham_DAO();
            var res = dao.get_category_id(id_loaisp);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateLoaiSP(FormCollection f, HttpPostedFileBase hinhanh)
        {
            if (f == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var id1 = f["a"];
            int id = Int32.Parse(f["a"]);
            var dao = new LoaiSanPham_DAO();
            LoaiSanPham lsp = new LoaiSanPham();
            var res = dao.gett_category_id(id);
            if (hinhanh == null)
            {
                if (res.hinhanh != null)
                {
                    lsp.id_loaisp = id;
                    lsp.tenloai = f["tenloai"];
                    lsp.tenngan = f["tenngan"];
                    lsp.thongtin = f["thongtin"];
                    lsp.hinhanh = res.hinhanh;
                    lsp.xeploai = f["xeploai"];
                    lsp.theloai = f["theloai"];
                    var res2 = dao.update_category_id(lsp);
                    var lstLoaiSanPham1 = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstLoaiSanPham1);
                }
                else
                {
                    lsp.id_loaisp = id;
                    lsp.tenloai = f["tenloai"];
                    lsp.tenngan = f["tenngan"];
                    lsp.thongtin = f["thongtin"];
                    lsp.hinhanh = "loaisp.png";
                    lsp.xeploai = f["xeploai"];
                    lsp.theloai = f["theloai"];
                    var res3 = dao.update_category_id(lsp);
                    var lstLoaiSanPham2 = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstLoaiSanPham2);
                }
            }
            //Kiem Tra hình tồn tại trong csdl chưa
            if (hinhanh.ContentLength > 0)
            {
                if (hinhanh.ContentType != "image/jpeg" && hinhanh.ContentType != "image/png" && hinhanh.ContentType != "image/gif" && hinhanh.ContentType != "image/tiff" && hinhanh.ContentType != "image/BMP" && hinhanh.ContentType != "image/jpg")
                {
                    ViewBag.upload = "không phải định dạng của hình ảnh";
                    return View();
                }
                //Lay hinh anh
                var fileName = Path.GetFileName(hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Source/img-public"), fileName);
                //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    lsp.hinhanh = fileName;
                }
                else
                {
                    //Lấy Hình Ảnh đưa vào thư muc HinhAnhSP
                    hinhanh.SaveAs(path);
                    lsp.hinhanh = fileName;
                }
            }
            lsp.id_loaisp = id;
            lsp.tenloai = f["tenloai"];
            lsp.tenngan = f["tenngan"];
            lsp.thongtin = f["thongtin"];
            lsp.xeploai = f["xeploai"];
            lsp.theloai = f["theloai"];
            var res1 = dao.update_category_id(lsp);
            var lstLoaiSanPham3 = dao.get_category_all();
            return View("QuanLyLoaiSanPham", lstLoaiSanPham3);
        }

        #endregion
        #region QuanLyKH
        public ActionResult QuanLyKhachHang()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }

            ViewBag.Name = session_acc.hoten;
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";

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
                ViewBag.notify = "Xoá Thành Công";
                ViewBag.Alert = "alert-info";
            }
            else
            {
                ViewBag.notify = "Xoá Thất Bại!";
                ViewBag.Alert = "alert-danger";
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
            return RedirectToAction("QuanLyKhachHang", tkngmua);
        }
        public ActionResult CreateBuyer(Account_buyer_Model acc)
        {
            if (acc == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new TaiKhoan_DAO();
            TaiKhoan res = dao.create_taikhoan(acc.username, CreateMD5(acc.password), acc.hoten, acc.email);

            if (res != null)
            {
                var id_taikhoan = res.id_taikhoan;
                var dao1 = new NguoiMua_DAO();
                NguoiMua res1 = dao1.create_nguoimua_Admin(id_taikhoan, acc.phone, acc.street, acc.ward, acc.district, acc.province);
                if (res1 != null)
                {
                    ViewBag.notify = "Thêm thành viên thành công";
                    ViewBag.Alert = "alert-info";
                }
                else
                {
                    ViewBag.notify = "Thêm thành viên thât bại";
                    ViewBag.Alert = "alert-danger";
                }
            }
            var dao2 = new NguoiMua_DAO();
            var lstNguoiMua = dao2.get_nguoimua_all();
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
            return RedirectToAction("QuanLyKhachHang", tkngmua);
        }
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
            dao1.update_pass(res.TaiKhoan.id_taikhoan, CreateMD5(sb.ToString()));
            return RedirectToAction("QuanLyKhachHang");
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
            dao.update_admin(res.TaiKhoan.id_taikhoan);
            return RedirectToAction("QuanLyKhachHang");
        }
        #endregion
        #region SanPham
        public ActionResult Products()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }

            ViewBag.Name = session_acc.hoten;
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";

            var dao = new SanPham_DAO();
            var dao1 = new LoaiSanPham_DAO();
            ViewBag.maloaisp = dao1.get_category_all_();
            var sp = dao.get_product_all_();
            return View(sp);
        }

        public ActionResult CreateSanPham(FormCollection f, HttpPostedFileBase[] hinhanh)
        {
            if (f == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (hinhanh == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = new SanPham();
            //Kiem Tra hình tồn tại trong csdl chưa
            int loi = 0;
            for (int i = 0; i < hinhanh.Count(); i++)
            {
                if (hinhanh[i] != null)
                {
                    //Kiem Tra noi dung hinh anh
                    if (hinhanh[i].ContentLength > 0)
                    {
                        if (hinhanh[i].ContentType != "image/jpeg" && hinhanh[i].ContentType != "image/png" && hinhanh[i].ContentType != "image/gif" && hinhanh[i].ContentType != "image/tiff" && hinhanh[i].ContentType != "image/BMP" && hinhanh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "Hình Ảnh" + i + "Không hợp lê <br />";
                            loi++;
                        }
                        else
                        {
                            //Lay hinh anh
                            var fileName = Path.GetFileName(hinhanh[i].FileName);
                            //Lấy hình ảnh chuyển vào thư mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Source/img-public/"), fileName);
                            //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo 
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload = "Hình " + i + " đã tồn tại";
                                loi++;
                                if (i == 0)
                                {
                                    sp.hinhanh1 = fileName;
                                }
                                else if (i == 1)
                                {
                                    sp.hinhanh2 = fileName;
                                }
                                else if (i == 2)
                                {
                                    sp.hinhanh3 = fileName;
                                }
                                else if (i == 3)
                                {
                                    sp.hinhanh4 = fileName;
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh1 = fileName;
                                }
                                else if (i == 1)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh2 = fileName;
                                }
                                else if (i == 2)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh3 = fileName;
                                }
                                else if (i == 3)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh4 = fileName;
                                }
                            }
                        }
                        
                    }
                }
            }
            var a = f["id_loaisp"];
            var b = f["soluong"];
            var c = f["dongia"];
            var d = f["giasale"];
            sp.tensp = f["tensp"];
            sp.id_loaisp = Int32.Parse(a);
            sp.tenngan = f["tenngan"];
            //sp.soluong = Int32.Parse(b);
            double dg;
            double.TryParse(c, out dg);
            sp.dongia = dg;

            double gias;
            double.TryParse(d, out gias);

            sp.giasale = gias;
            sp.trangthai = f["trangthai"];
            sp.hienthi = f["hienthi"];
            sp.tinhtrang = f["tinhtrang"];
            sp.thongtin = f["thongtin"];
            sp.xeploai = f["XepLoai"];
            var dao = new SanPham_DAO();
            var res = dao.set_product(sp);
            if (res != null)
            {
                a = f["chieurong"];
                b = f["chieudai"];
                c = f["chieucao"];
                d = f["cannang"];
                double r;
                double.TryParse(a, out r);
                double dd;
                double.TryParse(b, out dd);
                double cc;
                double.TryParse(c, out cc);
                double n;
                double.TryParse(d, out n);
                TheTich tt = new TheTich();
                tt.id_sanpham = res.id_sanpham;
                tt.chieurong = Convert.ToDouble(r);
                tt.chieudai = Convert.ToDouble(dd);
                tt.chieucao = Convert.ToDouble(cc);
                tt.cannang = Convert.ToDouble(n);
                var dao1 = new TheTich_DAO();
                var res1 = dao1.set_vol(tt);
                if (res1 == 1)
                {
                    ViewBag.notify = "Xoá Thành Cônh";
                    ViewBag.Alert = "alert-info";
                }
                else
                {
                    ViewBag.notify = "Xoá Thất Bại!";
                    ViewBag.Alert = "alert-danger";
                }
            }
            var sp1 = dao.get_product_all();
            return View("Products",sp1);
        }
        public JsonResult Get_TaiKhoan_id(int tk)
        {
            var dao = new TaiKhoan_DAO();
            var res_ = dao.Get_id_taikhoanAdmin(tk);
            return Json(res_, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get_SanPham_id(int id_sanpham)
        { 
            var dao = new SanPham_DAO();
            SanPham res_ = dao.get_product_idsanpham(id_sanpham);

            var dao1 = new TheTich_DAO();
            TheTich res1 = dao1.get_vol(id_sanpham); // volumn phải tồn tại

            SanPham_TheTich_Model lst = new SanPham_TheTich_Model();
            lst.id_sanpham = res_.id_sanpham;
            lst.tensp = res_.tensp;
            lst.id_loaisp = res_.id_loaisp;
            lst.tenngan = res_.tenngan;
            lst.soluong = res_.soluong;
            lst.dongia = res_.dongia;
            lst.giasale = res_.giasale;
            lst.trangthai = res_.trangthai;
            lst.hienthi = res_.hienthi;
            lst.tinhtrang = res_.tinhtrang;
            lst.thongtin = res_.thongtin;
            lst.xeploai = res_.xeploai;
            lst.chieucao = res1.chieucao;
            lst.chieurong = res1.chieurong;
            lst.chieudai = res1.chieudai;
            lst.cannang = res1.cannang;
            lst.hinhanh1 = res_.hinhanh1;
            lst.hinhanh2 = res_.hinhanh2;
            lst.hinhanh3 = res_.hinhanh3;
            lst.hinhanh4 = res_.hinhanh4;
            return Json(lst, JsonRequestBehavior.AllowGet);
        } //load editsanpham
        public ActionResult DeleteSanPham(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var dao = new HoaDonCT_DAO();
            var res = dao.get_hdct_all();
            foreach(var item in res)
            {
                var res2 = dao.remove_hdct_idsanpham(item.id_sanpham);
            }
            var dao1 = new SanPham_DAO();
            var res1 = dao1.remove_product(id);
            if (res1 != null)
            {
                ViewBag.notify = "Xoá Thành Công";
                ViewBag.Alert = "alert-info";
            }
            else
            {
                ViewBag.notify = "Xoá Thất Bại!";
                ViewBag.Alert = "alert-danger";
            }
            var sp1 = dao1.get_product_all();
            return View("Products", sp1);
        }
        public ActionResult UpdateSanPham(FormCollection f, HttpPostedFileBase[] hinhanh)
        {
            if (f == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = new SanPham();
            var dao = new SanPham_DAO();
            var idsp = f["id_sanpham"];
            var res1 = dao.get_product_idsanpham(Int32.Parse(idsp));
            //Kiem Tra hình tồn tại trong csdl chưa
            int loi = 0;
            for (int i = 0; i < hinhanh.Count(); i++)
            {
                if (hinhanh[i] != null)
                {
                    //Kiem Tra noi dung hinh anh
                    if (hinhanh[i].ContentLength > 0)
                    {
                        if (hinhanh[i].ContentType != "image/jpeg" && hinhanh[i].ContentType != "image/png" && hinhanh[i].ContentType != "image/gif" && hinhanh[i].ContentType != "image/tiff" && hinhanh[i].ContentType != "image/BMP" && hinhanh[i].ContentType != "image/jpg")
                        {
                            ViewBag.upload += "Hình Ảnh" + i + "Không hợp lê <br />";
                            loi++;
                        }
                        else
                        {
                            //Lay hinh anh
                            var fileName = Path.GetFileName(hinhanh[i].FileName);
                            //Lấy hình ảnh chuyển vào thư mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Source/img-public/"), fileName);
                            //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo 
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload = "Hình " + i + " đã tồn tại";
                                loi++;
                                if (i == 0)
                                {
                                    sp.hinhanh1 = fileName;
                                }
                                else if (i == 1)
                                {
                                    sp.hinhanh2 = fileName;
                                }
                                else if (i == 2)
                                {
                                    sp.hinhanh3 = fileName;
                                }
                                else if (i == 3)
                                {
                                    sp.hinhanh4 = fileName;
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh1 = fileName;
                                }
                                else if (i == 1)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh2 = fileName;
                                }
                                else if (i == 2)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh3 = fileName;
                                }
                                else if (i == 3)
                                {
                                    hinhanh[i].SaveAs(path);
                                    sp.hinhanh4 = fileName;
                                }
                            }
                        }

                    }
                }
                else
                {
                    loi++;
                    if (i == 0)
                    {
                        if (res1.hinhanh1 != null)
                        {  
                        sp.hinhanh1 = res1.hinhanh1;
                        }
                        else
                        {
                            sp.hinhanh1 = "loaisp.png";
                        }
                    }
                    else if (i == 1)
                    {
                        if (res1.hinhanh2 != null)
                        {
                            sp.hinhanh2 = res1.hinhanh2;
                        }
                        else
                        {
                            sp.hinhanh2 = "loaisp.png";
                        }
                    }
                    else if (i == 2)
                    {
                        if (res1.hinhanh1 != null)
                        {
                            sp.hinhanh3 = res1.hinhanh3;
                        }
                        else
                        {
                            sp.hinhanh3 = "loaisp.png";
                        }
                    }
                    else if (i == 3)
                    {
                        if (res1.hinhanh1 != null)
                        {
                            sp.hinhanh4 = res1.hinhanh4;
                        }
                        else
                        {
                            sp.hinhanh4 = "loaisp.png";
                        }
                    }
                }
            }
            var dao1 = new HoaDonCT_DAO();
            var res = dao1.get_hdct_all();
            foreach (var item in res)
            {
                var res2 = dao1.remove_hdct_idsanpham(item.id_sanpham);
            }
            var a = f["id_loaisp"];
            var b = f["soluong"];
            var c = f["dongia"];
            var d = f["giasale"];
            var e = f["id_sanpham"];
            double dg;
            double.TryParse(c, out dg);
            double gias;
            double.TryParse(d, out gias);
            if (b == "" || b!="0")
            {
                b = "1"; //b=""
            }
            var res3 = dao.update_product(Int32.Parse(e), f["tensp"], Int32.Parse(a), f["tenngan"], Int32.Parse(b),dg,gias, f["trangthai"], f["hienthi"], f["tinhtrang"], f["thongtin"], f["XepLoai"],sp.hinhanh1,sp.hinhanh2,sp.hinhanh3,sp.hinhanh4);
            if (res3 != null)
            {
                a = f["chieurong"];
                b = f["chieudai"];
                c = f["chieucao"];
                d = f["cannang"];
                double r;
                double.TryParse(a, out r);
                double dd;
                double.TryParse(b, out dd);
                double cc;
                double.TryParse(c, out cc);
                double n;
                double.TryParse(d, out n);
                var dao2 = new TheTich_DAO();
                var res4 = dao2.update_vol(res3.id_sanpham, r, dd, cc, n);
                if (res4 != null)
                {
                    ViewBag.notify = "Xoá Thành Công";
                    ViewBag.Alert = "alert-info";
                }
                else
                {
                    ViewBag.notify = "Xoá Thất Bại!";
                    ViewBag.Alert = "alert-danger";
                }
            }
            var sp1 = dao.get_product_all();
            return View("Products", sp1);
        }
        #endregion
        #region Size Mau
        public ActionResult QuanLySize_Mau()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }

            ViewBag.Name = session_acc.hoten;
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";

            var dao = new Color_DAO();
            var res = dao.get_color_all();
            var dao1 = new SanPham_DAO();
            ViewBag.sp = dao1.get_product_all();
            return View(res);
        }
        public ActionResult AddSize_Mau(FormCollection f)
        {
            var dao = new Color_DAO();
            var dao1 = new Size_DAO();
            var id_sanpham = f["id_sanpham"];
            var color = f["color"];
            var size = f["size"];
            var soluong = f["soluong"];
            var dao2 = new SanPham_DAO();
            var res2 = dao2.get_product__(Int32.Parse(id_sanpham));
            if (res2.soluong == null)
            {
                res2.soluong = Int32.Parse(soluong);
            }
            else
            {
                res2.soluong += Int32.Parse(soluong);
            }
            dao2.update_product_model(res2);
            var res1 = dao1.get_size_name(size, Int32.Parse(id_sanpham));
            if (res1 == null)
            {
                var sz=dao1.set_size_(size, Int32.Parse(id_sanpham), Int32.Parse(soluong));
                Color c = new Color();
                c.id_size = sz.id_size;
                c.color1 = color;
                c.soluong = Int32.Parse(soluong);
                dao.set_color(c);
            }
            else
            {
                res1.soluong += Int32.Parse(soluong);
                dao1.update_size(res1);
                Color c = new Color();
                c.id_size = res1.id_size;
                c.color1 = color;
                c.soluong = Int32.Parse(soluong);
                dao.checkupdate_color(c);
            }
            var res = dao.get_color_all();
            return View("QuanLySize_Mau",res);
        }

        [HttpGet]
        public JsonResult Get_Color_id(int id_color)
        {
            var dao = new Color_DAO();
            var res_ = dao.get_color_idColor(id_color);
            return Json(res_, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateSize_Color(FormCollection f)
        {
            if (f == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new Color_DAO();
            var id_color = Int32.Parse(f["id_color"]);
            var id_sanpham = Int32.Parse(f["id_sanpham"]);
            var color = f["color"];
            var size = f["size"];
            var soluong = Int32.Parse(f["soluong"]);
            var res1= dao.get_color_idColor(id_color);
            var dao2 = new SanPham_DAO();
            var res2 = dao2.get_product__(res1.Size.id_sanpham);
            res2.soluong -= res1.soluong;
            res2.soluong += soluong;
            dao2.update_product_model(res2);
            var dao1 = new Size_DAO();
            var res3 = dao1.get_size_name(res1.Size.size,res1.Size.id_sanpham);
            if (res3.size == size)
            {
                res3.soluong -= res1.soluong;
                res3.soluong += soluong;
            }
            else
            {
                var res4 = dao1.get_size_name(size, res1.Size.id_sanpham);
                if(res4 != null)
                {
                    res4.soluong += soluong;
                    res1.id_size = res4.id_size;
                    dao1.update_size(res4);
                }
                else
                {
                    var sz = dao1.set_size_(size, res1.Size.id_sanpham, soluong);
                    res1.id_size = sz.id_size;
                }
            }
            res1.color1 = color;
            res1.soluong = soluong;
            dao.set_color(res1);
            var res = dao.get_color_all();
            return View("QuanLySize_Mau", res);
        }
        public ActionResult DeleteSize_Color(int? id)
        {
            var dao = new Color_DAO();
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var res1 = dao.get_color_idColor(id);
            var dao2 = new SanPham_DAO();
            var res2 = dao2.get_product__(res1.Size.id_sanpham);
            res2.soluong -= res1.soluong;
            if (res2.soluong < 0)
            {
                res2.soluong = 0;
                res2.tinhtrang = "HẾT HÀNG";
            }
            else if (res2.soluong == 0)
            {
                res2.tinhtrang = "HẾT HÀNG";
            }
            dao2.update_product_model(res2);
            var dao1 = new Size_DAO();
            var res3 = dao1.get_size_name(res1.Size.size, res1.Size.id_sanpham);
            res3.soluong -= res1.soluong;
            if (res3.soluong <= 0)
            {
                res3 = dao1.get_size_name(res1.Size.size, res1.Size.id_sanpham);
                dao.remove_color(res1);
                dao1.remove_size(res3);
            }
            else
            {
                dao1.update_size(res3);
                dao.remove_color(res1);
            }
            var res = dao.get_color_all();
            return View("QuanLySize_Mau", res);
        }
        #endregion

        #region QuanLyHoaDon
        public ActionResult QuanLyHoaDon()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }

            ViewBag.Name = session_acc.hoten;
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";

            var dao = new HoaDon_DAO();
            var lst = dao.get_hoadon_damua_all();
            return View(lst);
        }
        public ActionResult HoaDonChiTiet(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new HoaDon_DAO();
            var res = dao.get_hoadon_idd(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            var dao1 = new HoaDonCT_DAO();
            var res1 = dao1.get_hdct_hoadon(id);
            ViewBag.ListHoaDonChiTiet = res1;

            var dao_namesize = new Size_DAO();
            //object model = new object();
            List<Size> model_total = new List<Size>();
            object model = new object();
            foreach (var m in res1)
            {
                var namesize = dao_namesize.get_size_id(m.id_size); //get name size
                model_total.Add(namesize);
            }
            ViewBag.lstSize = model_total;
            return View(res);
        }
        #endregion
        #region DauGia
        public ActionResult QuanLyDauGia()
        {
            var dao = new DauGia_DAO();
            var res = dao.get_DauGia_all();
            var dao1 = new SanPham_DAO();
            ViewBag.sp = dao1.get_product_all();
            return View(res);
        }
        public ActionResult DeleteDauGia(int id)
        {
            var dao = new DauGia_DAO();
            var res = dao.get_DauGia_id(id);
            if (res == "Xoá Thành Công")
            {
                ViewBag.Alert = "alert-info";
            }
            else
            {
                ViewBag.Alert = "alert-danger";
            }
            ViewBag.notify = res;
            var res1 = dao.get_DauGia_all();
            return View("QuanLyDauGia", res1);
        }
        public ActionResult AddDauGia(FormCollection dg)
        {
            var dao = new DauGia_DAO();
            DauGia daugia = new DauGia();
            daugia.id_sanpham = Int32.Parse(dg["id_sanpham"]);
            var a = dg["time_start"];
            var b = dg["time_end"];

            var start = DateTime.Parse(a);
            var end = DateTime.Parse(b);
            end = end.AddHours(23);
            end = end.AddMinutes(59);
            end = end.AddSeconds(59);
            if (start == null || end == null || start >= end || DateTime.Now > start)
            {
                return RedirectToAction("QuanLyDauGia");
            }
            daugia.status_ = "Mới Thêm";
            daugia.status_use = "Chưa Sử Dụng";
            daugia.time_start = start;
            daugia.time_end = end;
            var res = dao.setdaugia(daugia);
            var res1 = dao.get_DauGia_all();
            return RedirectToAction("QuanLyDauGia");
        }
        public ActionResult XemLichSuDG(int id)
        {
            var dao = new DauGia_DAO();
            var res = dao.get_DauGia_id_(id);
            var dao1 = new LichSuDG_DAO();
            ViewBag.lstLSDauGia = dao1.get_lsdg(id);
            return View(res);
        }
        public ActionResult StartDG(int id)
        {
            var dao = new DauGia_DAO();
            var res = dao.update_DauGia_id_(id);
            var res1 = dao.get_DauGia_all();
            return View("QuanLyDauGia", res1);
        }
        #endregion
        #region DuyetHD
        public JsonResult LoadHoaDonDuyet(int? id)
        {
            if (id != null)
            {
                var dao_ghn = new GHN_DAO();
                var res_ghn = dao_ghn.get_dhn_hoadon_id(id);
                if (res_ghn != null)
                {
                    return Json(res_ghn, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult LoadNguoiNhan(int id)
        {
            if (id <= 0)
            {
                var dao_hd = new HoaDon_DAO();
                var res_ghn = dao_hd.get_hoadon_id(id);

                var dao_ngm= new NguoiMua_DAO();
                var res_ngm = dao_ngm.get_nguoimua_id(id);
                if (res_ngm != null)
                {
                    return Json(res_ngm, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadNguoiGui()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(session_acc, JsonRequestBehavior.AllowGet);
            }
            
        }

        public async Task<ActionResult> CreateOrder_GHN(GHN_Ship ship)
        {
            var dao_ghn = new GHN_DAO();
            var res_ghn = dao_ghn.get_ghn_id(ship.id_ship);
            if (ship.note == null) ship.note = "Shippings";
            //object data = new
            //{
            //    payment_type_id = res_ghn.payment_type_id,
            //    note= ship.note,
            //    required_note= ship.required_note,
            //    return_phone= ship.return_phone,
            //    return_address= ship.return_address,
            //    return_district_id= 1450,
            //    return_ward_code= 20804,
            //    client_order_code= "",
            //    to_name= res_ghn.to_name,
            //    to_phone= res_ghn.to_phone,
            //    to_address= res_ghn.to_address,
            //    to_ward_code= res_ghn.to_ward_code,
            //    to_district_id= res_ghn.to_name_district,
            //    cod_amount= res_ghn.cod_amount,
            //    //content= res_ghn.,
            //    weight= res_ghn.weight*1000,
            //    length= res_ghn.length,
            //    width= res_ghn.width,
            //    height= res_ghn.height,
            //    pick_station_id= res_ghn.pick_station_id,
            //    insurance_value= res_ghn.insurance_value,
            //    service_id= res_ghn.service_id,
            //    //service_type_id= res_ghn.
            //};

            //var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var content_= "payment_type_id="+res_ghn.payment_type_id+"&note="+ship.note+ "&required_note="+ ship.required_note+
                            "&return_phone=" + ship.return_phone + "&return_address=" + ship.return_address + "&return_district_id=" + 1450 + "&return_ward_code="+ 20804 + "&client_order_code=null"+
                            "&to_name=" + res_ghn.to_name + "&to_phone=" + res_ghn.to_phone + "&to_address="+ res_ghn.to_address + "&to_ward_code="+ res_ghn.to_ward_code + "&to_district_id="+ res_ghn.to_name_district + "&cod_amount="+ res_ghn.cod_amount +
                            "&content=" + "sanphamtengi" +"&weight="+ res_ghn.weight*1000 + "&length="+ res_ghn.length + "&width="+ res_ghn.width + "&height="+ res_ghn.height + "&pick_station_id=" + res_ghn.pick_station_id + "&insurance_value="+ res_ghn.insurance_value + "&service_id="+ res_ghn.service_id;

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("token", "ef26267b-c8c7-11ea-b16d-9289328232ea");
            httpClient.DefaultRequestHeaders.Add("ShopId", "1257313");

            HttpResponseMessage result = httpClient.PostAsync("https://online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/create?"+content_,null).Result;
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                var a = result.Content.ReadAsStringAsync().Result;
                //var deserialized = JsonConvert.DeserializeObject<object>(response);
                string[] arrListStr = response.Split(new char[] { '\"','"'});

                var dao_hd = new HoaDon_DAO();
                var res_hd = dao_hd.update_hoadon_maghn(res_ghn.id_hoadon,arrListStr[11]);

                var dao_hdct = new HoaDonCT_DAO();
                var res_hdct = dao_hdct.get_hdct_hoadon_(res_ghn.id_hoadon); //trừ đi sl san pham

                if (arrListStr[11] == "" || arrListStr[11] == "0")
                {
                    return RedirectToAction("QuanLyHoaDon");
                }

                return RedirectToAction("QuanLyHoaDon");
            }

            return RedirectToAction("QuanLyHoaDon");
        }
        #endregion
        #region Ma Hoa MD5
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
        #endregion
        #region Slider
        public ActionResult Slider()
        {
            var dao = new Slider_DAO();
            var res = dao.slider_get_all();
            ViewBag.slider = res;
            return View();
        }
        [HttpGet]
        public JsonResult Get_Slider_id(int id_slider)
        {
            var dao = new Slider_DAO();
            var res = dao.get_slider_id(id_slider);
            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Slider(FormCollection f, HttpPostedFileBase hinhanh)
        {
            var id = f["id_slider"];
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var dao = new Slider_DAO();
            if (hinhanh == null)
            {
                var slider = new Slider();
                var res = dao.get_slider_id(Int32.Parse(id));
                slider.id_slider = res.id_slider;
                slider.image = res.image;
                slider.sale = f["sale"];
                slider.title = f["title"];
                slider.description = f["description"];
                if (slider == res)
                {
                    ViewBag.notify = "Dữ liệu bị trùng";
                    ViewBag.Alert = "alert-warning";
                    return View();
                }
                else
                {
                    var res1 = dao.set_slider(slider);
                    if (res1 == 1)
                    {
                        ViewBag.notify = "Thay đổi thành công";
                        ViewBag.Alert = "alert-info";
                    }
                    else
                    {
                        ViewBag.notify = "Thay đổi thất bại";
                        ViewBag.Alert = "alert-danger";
                    }
                }

            }
            else if (hinhanh.ContentLength > 0)
            {
                if (hinhanh.ContentType != "image/jpeg" && hinhanh.ContentType != "image/png" && hinhanh.ContentType != "image/gif" && hinhanh.ContentType != "image/tiff" && hinhanh.ContentType != "image/BMP" && hinhanh.ContentType != "image/jpg")
                {
                    ViewBag.notify = "không phải định dạng của hình ảnh";
                    ViewBag.Alert = "alert-danger";
                    return View();
                }
                //Lay hinh anh
                var fileName = Path.GetFileName(hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Source/Images"), fileName);
                //Nếu thư mục chứa hình ảnh đó rồi
                if (System.IO.File.Exists(path))
                {
                    var res = dao.get_slider_id(Int32.Parse(id));
                    res.sale = f["sale"];
                    res.title = f["title"];
                    res.description = f["description"];
                    var res1 = dao.set_slider(res);
                    if (res1 == 1)
                    {
                        ViewBag.notify = "Thay đổi thành công";
                        ViewBag.Alert = "alert-info";
                    }
                    else
                    {
                        ViewBag.notify = "Thay đổi thât bại";
                        ViewBag.Alert = "alert-danger";
                    }
                }
                else
                {
                    var res = dao.get_slider_id(Int32.Parse(id));
                    var path1 = Path.Combine(Server.MapPath("~/Source/Images"), res.image);
                    System.IO.File.Delete(path1);
                    //Lấy Hình Ảnh đưa vào thư muc HinhAnhSP
                    hinhanh.SaveAs(path);
                    res.image = fileName;
                    res.sale = f["sale"];
                    res.title = f["title"];
                    res.description = f["description"];
                    var res1 = dao.set_slider(res);
                    if (res1 == 1)
                    {
                        ViewBag.notify = "Thay đổi thành công";
                        ViewBag.Alert = "alert-info";
                    }
                    else
                    {
                        ViewBag.notify = "Thay đổi thât bại";
                        ViewBag.Alert = "alert-danger";
                    }
                }
            }
            var res2 = dao.slider_get_all();
            ViewBag.slider = res2;
            return View();
        }
        public void Urlpaypal(string magiaodich)
        {
            Response.Redirect("https://www.sandbox.paypal.com/activity/payment/" + magiaodich);
        }
        #endregion
    }
}