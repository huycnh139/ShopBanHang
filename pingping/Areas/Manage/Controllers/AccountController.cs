using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace pingping.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        // GET: Manage/Account
        public ActionResult Login()
        {
            return View();
        }
    }
}