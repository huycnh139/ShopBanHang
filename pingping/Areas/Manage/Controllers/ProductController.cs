using Modal.DAO;
using Modal.EF;
using pingping.Common;
using pingping.Models;
using pingping.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;

namespace pingping.Areas.Manage.Controllers
{
    public class ProductController : Controller
    {
        // GET: Manage/Product
        public ActionResult QuanLySanPham()
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null)
            {
                return RedirectToAction("../../Accounts/Login");
            }
            else if (session_acc.loaitk)
                return RedirectToAction("../../Home/Index");
            ViewBag.HoTen = session_acc.hoten;
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
                            var fileName = Path.GetFileNameWithoutExtension(hinhanh[i].FileName);
                            var extention = Path.GetExtension(hinhanh[i].FileName);
                            //Lấy hình ảnh chuyển vào thư mục hình ảnh
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
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
                    ViewBag.notify = 1;
                    ViewBag.action = "Thêm Sản Phẩm Thành Công";
                }
                else
                {
                    ViewBag.notify = -1;
                    ViewBag.action = "Thêm Sản Phẩm Thất Bại";
                }
            }
            
            var dao2 = new LoaiSanPham_DAO();
            ViewBag.maloaisp = dao2.get_category_all_();
            var sp1 = dao.get_product_all_();
            return View("QuanLySanPham", sp1);
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
            foreach (var item in res)
            {
                var res2 = dao.remove_hdct_idsanpham(item.id_sanpham);
            }
            var dao1 = new SanPham_DAO();
            var res1 = dao1.remove_product(id);
            if (res1 != null)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Xoá Sản Phẩm Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Thêm Sản Phẩm Thất Bại";
            }
            var dao5 = new SanPham_DAO();
            var dao6 = new LoaiSanPham_DAO();
            ViewBag.maloaisp = dao6.get_category_all_();
            var sp1 = dao5.get_product_all_();
            return View("QuanLySanPham", sp1);
        }
        public ActionResult TDSanPham(FormCollection f, HttpPostedFileBase[] hinhanh)
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
                            var fileName = Path.GetFileNameWithoutExtension(hinhanh[i].FileName);
                            var extention = Path.GetExtension(hinhanh[i].FileName);
                            //Lấy hình ảnh chuyển vào thư mục hình ảnh
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                            //Lấy hình ảnh chuyển vào thư mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Source/img-public/"), fileName);
                            //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo 
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload = "Hình " + i + " đã tồn tại";
                                loi++;
                                if (i == 0)
                                {
                                    if (res1.hinhanh1 != null)
                                    {
                                        var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res1.hinhanh1);
                                        System.IO.File.Delete(imgdelete);
                                    }
                                    sp.hinhanh1 = fileName;
                                }
                                else if (i == 1)
                                {
                                    if (res1.hinhanh2 != null)
                                    {
                                        var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res1.hinhanh2);
                                        System.IO.File.Delete(imgdelete);
                                    }
                                    sp.hinhanh2 = fileName;
                                }
                                else if (i == 2)
                                {
                                    if (res1.hinhanh3 != null)
                                    {
                                        var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res1.hinhanh3);
                                        System.IO.File.Delete(imgdelete);
                                    }
                                    sp.hinhanh3 = fileName;
                                }
                                else if (i == 3)
                                {
                                    if (res1.hinhanh4 != null)
                                    {
                                        var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res1.hinhanh4);
                                        System.IO.File.Delete(imgdelete);
                                    }
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
            if (b == "" || b != "0")
            {
                b = "1"; //b=""
            }
            var res3 = dao.update_product(Int32.Parse(e), f["tensp"], Int32.Parse(a), f["tenngan"], Int32.Parse(b), dg, gias, f["trangthai"], f["hienthi"], f["tinhtrang"], f["thongtin"], f["XepLoai"], sp.hinhanh1, sp.hinhanh2, sp.hinhanh3, sp.hinhanh4);
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
                    ViewBag.notify = 1;
                    ViewBag.action = "Sửa Sản Phẩm Thành Công";
                }
                else
                {
                    ViewBag.notify = -1;
                    ViewBag.action = "Sửa Sản Phẩm Thất Bại";
                }
            }

            var dao5 = new SanPham_DAO();
            var dao6 = new LoaiSanPham_DAO();
            ViewBag.maloaisp = dao6.get_category_all_();
            var sp1 = dao5.get_product_all_();
            return View("QuanLySanPham", sp1);
        }

        public ActionResult QuanLySize_Mau()
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
                var sz = dao1.set_size_(size, Int32.Parse(id_sanpham), Int32.Parse(soluong));
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

            ViewBag.notify = 1;
            ViewBag.action = "Thêm Thành Công";

            var dao5 = new Color_DAO();
            var cl = dao5.get_color_all();
            var dao6 = new SanPham_DAO();
            ViewBag.sp = dao6.get_product_all();
            return View("QuanLySize_Mau", cl);
        }
        [HttpGet]
        public JsonResult Get_Product_search(string stuKhoa)
        {
            var dao_sp = new SanPham_DAO();
            var res_sp = dao_sp.get_search_product_(stuKhoa);
            return Json(res_sp, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Get_Color_id(int id_color)
        {
            var dao_c = new Color_DAO();
            var res_c = dao_c.get_color_id(id_color);
            var dao_s = new Size_DAO();
            var res_s = dao_s.get_size_id_js(res_c.id_size);

            var dao_p = new SanPham_DAO();
            var res_p = dao_p.get_product_idsanpham(res_s.id_sanpham);

            Color_Size lst = new Color_Size();
            lst.id_sanpham = res_p.id_sanpham;
            lst.id_color = res_c.id_color;
            lst.tensp = res_p.tensp;
            lst.size = res_s.size;
            lst.color = res_c.color1;
            lst.soluong = res_c.soluong;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Random_Size_Color(string tensp)
        {
            Color_Size lst = new Color_Size();
            var dao_sp = new SanPham_DAO();
            var listSize = new List<string> { "XS", "S", "M", "L","XL","XXL" };
            var listColor = new List<string> { "Đỏ", "Xanh", "Vàng", "Tím", "Hồng", "Trắng", "Nâu", "Đen" };
            var listsp = dao_sp.get_product_category();
            var random = new Random();
            int size = random.Next(listSize.Count);
            int soluong = random.Next(1, 50);
            int color= random.Next(listColor.Count);
            int sp = random.Next(listsp.Count());
            lst.tensp = tensp;
            lst.id_sanpham = listsp[sp].id_sanpham;
            lst.size = listSize[size];
            lst.color = listColor[color];
            lst.soluong = soluong;
            return Json(lst, JsonRequestBehavior.AllowGet);
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
            var res1 = dao.get_color_idColor(id_color);
            var dao2 = new SanPham_DAO();
            var res2 = dao2.get_product__(res1.Size.id_sanpham);
            res2.soluong -= res1.soluong;
            res2.soluong += soluong;
            dao2.update_product_model(res2);
            var dao1 = new Size_DAO();
            var res3 = dao1.get_size_name(res1.Size.size, res1.Size.id_sanpham);
            if (res3.size == size)
            {
                res3.soluong -= res1.soluong;
                res3.soluong += soluong;
                dao1.update_size(res3);
            }
            else
            {
                var res4 = dao1.get_size_name(size, res1.Size.id_sanpham);
                if (res4 != null)
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
            dao.update_color(res1);

            ViewBag.notify = 1;
            ViewBag.action = "Sửa Thành Công";
            var dao5 = new Color_DAO();
            var cl = dao5.get_color_all();
            var dao6 = new SanPham_DAO();
            ViewBag.sp = dao6.get_product_all();
            return View("QuanLySize_Mau", cl);
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

            ViewBag.notify = 1;
            ViewBag.action = "Xoá Thành Công";
            var dao5 = new Color_DAO();
            var cl = dao5.get_color_all();
            var dao6 = new SanPham_DAO();
            ViewBag.sp = dao6.get_product_all();
            return View("QuanLySize_Mau", cl);
        }
        public JsonResult reSearch_Product(string stuKhoa)
        {
            List<Color_Size> result = new List<Color_Size>();
            var dao_color = new Color_DAO();
            var result_color = dao_color.get_reSearch_product(stuKhoa);
            if (result_color.Count() > 0)
            {
                IQueryable<Color> lst = result_color;
                var dao_size = new Size_DAO();
                foreach (var item in lst)
                {
                    var result_size = dao_size.get_size_(item.id_size);
                    if (result_size != null)
                    {
                        Color_Size CS = new Color_Size();
                        var dao_product = new SanPham_DAO();
                        var result_product = dao_product.get_product_idsanpham(result_size.id_sanpham);
                        CS.id_sanpham = result_product.id_sanpham;
                        CS.id_color = item.id_color;
                        CS.tensp = result_product.tensp;
                        CS.size = result_size.size;
                        CS.color = item.color1;
                        CS.soluong = item.soluong;
                        CS.soluong_size = result_size.soluong;
                        CS.soluong_sp = result_product.soluong;
                        result.Add(CS);
                    }
                }
            }
           

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}