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

        public HomeController(ISetupRepository setupRepository)
        {
            _setupRepository = setupRepository;
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
                    HttpContext.Session.SetInt32("UserId", result.Id);
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
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", new { Controller = "Home", Area = "Admin" });
        }
    }
}
