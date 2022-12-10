using Infrastructure.Utilities;
using Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAppUserRepository
    {
        Task<bool> AppUserSignUp(AppUserSignUpVM model);
        Task<bool> AppUserUpdate(AppUserUpdateVM model);
        Task<AppUserVM> AppUserSignIn(AppUserSignin model);
        Task<AppUserVM> AppUserGetById(int id);
        Task<PagingModel<AppUserVM>> AppUsersGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
    }
}
