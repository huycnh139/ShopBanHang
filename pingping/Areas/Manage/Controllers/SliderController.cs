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
    public class SliderController : Controller
    {
        // GET: Manage/Slider

        public ActionResult Slider()
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

                    ViewBag.notify = -1;
                    ViewBag.action = "Dữ liệu bị trùng";
                    

                    var dao8 = new Slider_DAO();
                    var res8 = dao.slider_get_all();
                    ViewBag.slider = res8;
                    return View("Slider");
                }
                else
                {
                    var res1 = dao.set_slider(slider);
                    if (res1 == 1)
                    {

                        ViewBag.notify = 1;
                        ViewBag.action = "Thay đổi thành công";
                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");
                    }
                    else
                    {

                        ViewBag.notify = -1;
                        ViewBag.action = "Thay đổi thất bại";
                        
                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");
                    }
                }

            }
            else if (hinhanh.ContentLength > 0)
            {
                if (hinhanh.ContentType != "image/jpeg" && hinhanh.ContentType != "image/png" && hinhanh.ContentType != "image/gif" && hinhanh.ContentType != "image/tiff" && hinhanh.ContentType != "image/BMP" && hinhanh.ContentType != "image/jpg")
                {
                    ViewBag.notify = -1;
                    ViewBag.action = "không phải định dạng của hình ảnh";
                    var dao8 = new Slider_DAO();
                    var res8 = dao.slider_get_all();
                    ViewBag.slider = res8;
                    return View("Slider");
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

                        ViewBag.notify = 1;
                        ViewBag.action = "Thay đổi thành công";

                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");
                    }
                    else
                    {

                        ViewBag.notify = -1;
                        ViewBag.action = "Thay đổi thât bại";

                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");

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

                        ViewBag.notify = 1;
                        ViewBag.action = "Thay đổi thành công";

                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");
                    }
                    else
                    {

                        ViewBag.notify = -1;
                        ViewBag.action = "Thay đổi thât bại";

                        var dao8 = new Slider_DAO();
                        var res8 = dao.slider_get_all();
                        ViewBag.slider = res8;
                        return View("Slider");
                    }
                }
            }
            var res2 = dao.slider_get_all();
            ViewBag.slider = res2;
            return View();
        }
    }
}