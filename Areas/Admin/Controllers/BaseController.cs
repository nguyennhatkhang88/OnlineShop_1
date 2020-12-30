using OnLineShop_1.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnLineShop_1.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Session[CommonContants.CurrentCulture] != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session[CommonContants.CurrentCulture].ToString());
                
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session[CommonContants.CurrentCulture].ToString());
            }
            else
            {
                Session[CommonContants.CurrentCulture] = "VN";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("VN");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("VN");
            }
        }

        public ActionResult ChangeCulture(string ddlCulture, string returnUrl)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(ddlCulture);
             //Thread.CurrentThread.CurrentUICulture = new CultureInfo(ddlCulture);
            Session[CommonContants.CurrentCulture] = ddlCulture;
            return Redirect(returnUrl);
        }
        // GET: Admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonContants.USER_SEESION];
            if(session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index", Areas = "Admin" }));
            }
            base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}