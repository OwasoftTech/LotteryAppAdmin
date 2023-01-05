using GUI.Utilities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : BaseController
    {
        private ISetupRepository _setupRepository;

        public AccountController(ISetupRepository setupRepository)
        {
            _setupRepository = setupRepository;
        }
        #region User Roles
        public IActionResult Roles()
        {
            return View();
        }
        public async Task<IActionResult> UserRolesPartial(int PageIndex = 1, int PageSize = 10, string SearchTerm = "")
        {
            return PartialView("_UserRolesPartial", await _setupRepository.UserRolesGetAll(SearchTerm, PageIndex, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> UserRoleAddUpdate(int id)
        {
            if (id > 0)
            {
                var model = await _setupRepository.UserRoleGetById(id);
                return PartialView("_UserRoleAddUpdate", model);
            }
            else
            {
                return PartialView("_UserRoleAddUpdate", new UserRoleVM());
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserRoleAddUpdate(UserRoleVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var resutl = await _setupRepository.UserRoleUpdate(model);
                    if (resutl == true)
                    {
                        return Json("UserRolePartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
                else
                {
                    var resutl = await _setupRepository.UserRoleCreate(model);
                    if (resutl == true)
                    {
                        return Json("UserRolePartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
            }
            else
            {
                return Json("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserRoleDelete(int id)
        {
            var result = await _setupRepository.UserRoleDelete(id);
            if (result == true)
            {
                return Json("UserRolePartialButton");
            }
            else
            {
                Json("error");
            }
            return Json("");
        }
        #endregion
        #region Users
        public IActionResult Users()
        {
            return View();
        }
        public async Task<IActionResult> UserPartial(int PageIndex = 1, int PageSize = 10, string SearchTerm = "")
        {
            return PartialView("_UserPartial", await _setupRepository.UserGetAll(SearchTerm, PageIndex, PageSize));
        }
        [HttpGet]
        public async Task<IActionResult> UserAddUpdate(int id)
        {
            var Roles = await _setupRepository.UserRolesAll();
            if (id > 0)
            {
                var model = await _setupRepository.UserGetById(id);
                return PartialView("_UserAddUpdate", model);
            }
            else
            {
                var model = new UserVM();
                if (Roles != null && Roles.Count() > 0)
                {
                    model.Roles = Roles;
                }
                return PartialView("_UserAddUpdate", model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UserAddUpdate(UserVM model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var resutl = await _setupRepository.UserUpdate(model);
                    if (resutl == true)
                    {
                        return Json("UserPartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
                else
                {
                    var resutl = await _setupRepository.UserCreate(model);
                    if (resutl == true)
                    {
                        return Json("UserPartialButton");
                    }
                    else
                    {
                        return Json("error");
                    }
                }
            }
            else
            {
                return Json("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UserDelete(int id)
        {
            var result = await _setupRepository.UserDelete(id);
            if (result == true)
            {
                return Json("UserPartialButton");
            }
            else
            {
                Json("error");
            }
            return Json("");
        }
        #endregion


    }
}
