using GUI.Utilities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUserController : BaseController
    {
        private IAppUserRepository _appUserRepository;

        public AppUserController(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }
        public IActionResult AppUsers()
        {
            return View();
        }
        public async Task<IActionResult> AppUsersPartial(int PageIndex = 1, int PageSize = 10, string SearchTerm = "")
        {
            return PartialView("_AppUsersPartial", await _appUserRepository.AppUsersGetAll(SearchTerm, PageIndex, PageSize));
        }

    }
}
