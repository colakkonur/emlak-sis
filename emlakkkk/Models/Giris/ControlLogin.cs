using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlakkkk.Models.Giris
{
    public class ControlLogin:ActionFilterAttribute,IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["userid"].ToString()))
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/login");
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/login");
            }
        }
    }
}