using Modal.DAO;
using Modal.EF;
using pingping.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace pingping.Areas.Manage.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Manage/Category
        public ActionResult QuanLyLoaiSanPham()
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

            var res1 = dao.gett_category_id(id);
            if (res1.hinhanh != null)
            {
                var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res1.hinhanh);
                System.IO.File.Delete(imgdelete);
            }

            var res = dao.remove_category_idloaisanpham(id);
            if (res == 1)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Xoá Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Xoá Thất Bại";
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
                    ViewBag.notify = -1;
                    ViewBag.action = "Không phải định dạng của hình ảnh";
                    var lstcategory = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstcategory);
                }
                //Lay hinh anh
                var fileName = Path.GetFileNameWithoutExtension(hinhanh.FileName);
                var extention = Path.GetExtension(hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Source/img-public"), fileName);
                //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    ViewBag.notify = -1;
                    ViewBag.action = "Hình đã tồn tại";
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
                        ViewBag.notify = 1;
                        ViewBag.action = "Thêm loại sản phẩm thành công";
                    }
                    else
                    {
                        ViewBag.notify = -1;
                        ViewBag.action = "Thêm loại sản phẩm thất bại";
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
                    dao.update_category_id(lsp);
                    ViewBag.notify = 1;
                    ViewBag.action = "Sửa sản phẩm thành công";
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
                    dao.update_category_id(lsp);
                    ViewBag.notify = 1;
                    ViewBag.action = "Sửa sản phẩm thành công";
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

                    ViewBag.notify = -1;
                    ViewBag.action = "Không phải định dạng của hình ảnh";
                    var lstLoaiSanPham2 = dao.get_category_all();
                    return View("QuanLyLoaiSanPham", lstLoaiSanPham2);
                }
                //Lay hinh anh
                var fileName = Path.GetFileNameWithoutExtension(hinhanh.FileName);
                var extention = Path.GetExtension(hinhanh.FileName);
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                //Lấy hình ảnh chuyển vào thư mục hình ảnh
                var path = Path.Combine(Server.MapPath("~/Source/img-public"), fileName);
                //Nếu thư mục chứa hình ảnh đó rồi sẽ xuất ra thông báo
                if (System.IO.File.Exists(path))
                {
                    lsp.hinhanh = fileName;
                }
                else
                {
                    if (res.hinhanh != null)
                    {
                        var imgdelete = Path.Combine(Server.MapPath("~/Source/img-public"), res.hinhanh);
                        System.IO.File.Delete(imgdelete);
                    }
                    //Lấy Hình Ảnh đưa vào thư muc 
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
            ViewBag.notify = 1;
            ViewBag.action = "Sửa Thành Công";
            var lstLoaiSanPham3 = dao.get_category_all();
            return View("QuanLyLoaiSanPham", lstLoaiSanPham3);
        }
    }
}