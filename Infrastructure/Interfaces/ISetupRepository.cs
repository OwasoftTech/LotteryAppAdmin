using DbModels;
using Infrastructure.Utilities;
using Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISetupRepository
    {
        #region User Roles
        Task<PagingModel<UserRoleVM>> UserRolesGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
        Task<UserRoleVM> UserRoleGetById(int id);
        Task<bool> UserRoleCreate(UserRoleVM model);
        Task<bool> UserRoleUpdate(UserRoleVM model);
        Task<bool> UserRoleDelete(int id);
        Task<IEnumerable<UserRoleVM>> UserRolesAll();
        #endregion
        #region Users
        Task<PagingModel<UserVM>> UserGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
        Task<bool> UserCreate(UserVM model);
        Task<bool> UserUpdate(UserVM model);
        Task<bool> UserDelete(int id);
        Task<UserVM> UserGetById(int id);
        Task<User> Login(LoginVM model);
        Task<IEnumerable<DashboardCountVM>> DashboardCount();
        #endregion
        //Task<PagingModel<UserVM>> UserGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
    }
}
