using Modal.DAO;
using Modal.EF;
using pingping.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pingping.Areas.Manage.Controllers
{
    public class AuctionController : Controller
    {
        // GET: Manage/Auction
        public ActionResult QuanLyDauGia()
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
                ViewBag.notify = 1;
                ViewBag.action = "Xoá Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Xoá Thất Bại";
            }


            var dao3 = new DauGia_DAO();
            var res3 = dao3.get_DauGia_all();
            var dao4 = new SanPham_DAO();
            ViewBag.sp = dao4.get_product_all();
            return View("QuanLyDauGia", res3);
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
                ViewBag.notify = -1;
                ViewBag.action = "Chọn Thời Gian Không Phù Hợp";

                var dao3 = new DauGia_DAO();
                var res3 = dao3.get_DauGia_all();
                var dao4 = new SanPham_DAO();
                ViewBag.sp = dao4.get_product_all();
                return View("QuanLyDauGia", res3);
            }
            daugia.status_ = "Mới Thêm";
            daugia.status_use = "Chưa Sử Dụng";
            daugia.time_start = start;
            daugia.time_end = end;
            var res = dao.setdaugia(daugia);

            ViewBag.notify = 1;
            ViewBag.action = "Thêm Mới Thành Công";

            var dao2 = new DauGia_DAO();
            var res2 = dao2.get_DauGia_all();
            var dao5 = new SanPham_DAO();
            ViewBag.sp = dao5.get_product_all();
            return View("QuanLyDauGia", res2);
        }
        public ActionResult XemLichSuDG(int id)
        {
            var session_acc = SessionHelper.GetSession();
            if (session_acc == null )
            {
                return RedirectToAction("../../Accounts/Login");
            }
            else if(session_acc.loaitk)
                return RedirectToAction("../../Home/Index");
            ViewBag.Messager = "Đăng Xuất";
            ViewBag.Login = "../Accounts/Logout";
            ViewBag.HoTen = session_acc.hoten;

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
            if (res != null)
            {
                ViewBag.notify = 1;
                ViewBag.action = "Bắt đầu Thành Công";
            }
            else
            {
                ViewBag.notify = -1;
                ViewBag.action = "Bắt đầu Thất Bại";
            }
            var dao2 = new DauGia_DAO();
            var res2 = dao2.get_DauGia_all();
            var dao5 = new SanPham_DAO();
            ViewBag.sp = dao5.get_product_all();
            return View("QuanLyDauGia", res2);
        }
    }
}