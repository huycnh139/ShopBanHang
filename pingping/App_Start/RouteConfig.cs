using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pingping
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "sanpham",
                url: "san-pham",
                defaults: new { controller = "Home", action = "ProductPage", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "lienhe",
                url: "lien-he",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "giohang",
                url: "xem-gio-hang-chi-tiet",
                defaults: new { controller = "Home", action = "CheckOut", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "donhangcuatoi",
                url: "don-hang-da-mua",
                defaults: new { controller = "Home", action = "MyOrder", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "trangchu",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "dangnhap",
                url: "dang-nhap",
                defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "dangky",
                url: "dang-ky",
                defaults: new { controller = "Accounts", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "quenmk",
                url: "quen-mat-khau",
                defaults: new { controller = "Accounts", action = "Forgotpassword", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "payment",
                url: "thanh-toan",
                defaults: new { controller = "Home", action = "Payment", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "xemchitiet",
                url: "{tenngan}-{id}",
                defaults: new { controller = "Home", action = "SingleProcduct", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
