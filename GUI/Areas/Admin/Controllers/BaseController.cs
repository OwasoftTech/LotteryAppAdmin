using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        private int theUser;

        public int TheUser
        {
            get { return theUser; }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Session.GetInt32("UserId").HasValue)
            {
               // context.Result = new RedirectToRouteResult(
                 //     new { area = "Admin", controller = "Home", action = "Index" });
            }
            else
            {
                theUser = context.HttpContext.Session.GetInt32("UserId").Value;
                base.OnActionExecuting(context);
            }
        }
    }
}
