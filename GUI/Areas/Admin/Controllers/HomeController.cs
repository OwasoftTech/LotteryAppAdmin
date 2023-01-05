using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private ISetupRepository _setupRepository;
        private IHttpContextAccessor _httpContextAccessor;

        public HomeController(ISetupRepository setupRepository , IHttpContextAccessor  httpContextAccessor)
        {
            _setupRepository = setupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Infrastructure.ViewModels.LoginVM model)
        {
            if(ModelState.IsValid)
            {
                var result =await _setupRepository.Login(model);
                if(result != null)
                {
                    //_httpContextAccessor.HttpContext.Session.SetInt32("UserId", result.Id);
                    HttpContext.Session.SetInt32("UserId", result.Id);
                    //HttpContext.Session.SetString("UserId", result.Id.ToString());

                    return Json("success");
                }
                else
                {
                    return Json("fail");
                }

            }
            return View("");
        }
        public IActionResult Logout()
        {
            //HttpContext.Session.Clear();
            //HttpContext.Session.Remove("UserId");
           _httpContextAccessor.HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", new { Controller = "Home", Area = "Admin" });
        }
    }
}
