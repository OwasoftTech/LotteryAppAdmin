using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace GUI.Utilities
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            if (filterContext.HttpContext.Session.GetString("UserId") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                      new { area = "Admin", controller = "Home", action = "Index" });
            }

        }
    }
}
