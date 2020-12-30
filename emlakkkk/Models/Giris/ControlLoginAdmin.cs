using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlakkkk.Models.Giris
{
    public class ControlLoginAdmin : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Session["userid"].ToString()))
                {
                    if (HttpContext.Current.Session["usertype"].ToString() == "admin")
                    {
                        base.OnActionExecuting(filterContext);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("/admin");
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/admin");
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect("/admin");
            }
        }
    }
}