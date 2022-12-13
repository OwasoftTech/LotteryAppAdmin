using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MainController : BaseController
    {
        private ISetupRepository _setupRepository;

        public MainController(ISetupRepository setupRepository)
        {
            _setupRepository = setupRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _setupRepository.DashboardCount());
        }
        public IActionResult SyncSession()
        {
            return Json("success");
        }
    }
}
