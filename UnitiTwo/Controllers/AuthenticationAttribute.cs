using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UnitiTwo.Controllers
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["username"] == null)
                filterContext.Result = new RedirectToRouteResult("Default", new System.Web.Routing.RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Home"
                }));
            base.OnActionExecuting(filterContext);
        }
    }
}