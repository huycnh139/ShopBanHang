using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pingping.Common
{
    public class SessionHelper
    {
        public static void SetSession(AccLogin acc)
        {
            HttpContext.Current.Session["Account_Session"] = acc;
        }
        public static AccLogin GetSession()
        {
            var session = HttpContext.Current.Session["Account_Session"];
            if (session == null)
            {
                return null;
            }
            else
            {
                return session as AccLogin;
            }
        }
        public static void ClearSession()
        {
            HttpContext.Current.Session.Abandon();
        }
    }
}