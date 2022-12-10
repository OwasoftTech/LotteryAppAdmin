using DbModels;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class SetupRepository : ISetupRepository
    {
        private LotteryAppDBContext _context;
        private IGenericRepository _genericRepository;

        public SetupRepository(LotteryAppDBContext context, IGenericRepository genericRepository)
        {
            _context = context;
            _genericRepository = genericRepository;
        }
        #region User Roles
        public async Task<PagingModel<UserRoleVM>> UserRolesGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10)
        {
            return await _genericRepository.GetPagedData<UserRoleVM>($@"[{DbSchema.Setup}].[{nameof(UserRolesGetAll)}]", PageIndex, PageSize, SearchTerm, GetAllParms.NotDeleted, GetAllParms.TempId);
        }

        public async Task<UserRole> UserRoleEntity(int id)
        {
            return await _genericRepository.GetDataById<UserRole>($@"[{DbSchema.Setup}].[{nameof(UserRolesGetAll)}]", GetAllParms.PageIndex, GetAllParms.PageSize, GetAllParms.SearchTerm, GetAllParms.NotDeleted, id);
        }
        public async Task<UserRoleVM> UserRoleGetById(int id)
        {
            var Entity = await UserRoleEntity(id);
            if (Entity == null)
            {
                return new UserRoleVM();
            }
            else
            {
                return new UserRoleVM()
                {
                    Id = Entity.Id,
                    Name = Entity.Name
                };
            }
        }
        public async Task<bool> UserRoleCreate(UserRoleVM model)
        {
            var Entity = new UserRole()
            {
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Name = model.Name
            };
            return await _genericRepository.Create(Entity);
        }
        public async Task<bool> UserRoleUpdate(UserRoleVM model)
        {
            var Entity = await UserRoleEntity(model.Id);
            if (Entity == null)
            {
                return false;
            }
            else
            {
                Entity.Name = model.Name;
                Entity.UpdatedDate = DateTime.Now;
                return await _genericRepository.Update(Entity);
            }
        }
        public async Task<bool> UserRoleDelete(int id)
        {
            var Entity = await UserRoleEntity(id);
            if (Entity == null)
            {
                return false;
            }
            else
            {
                Entity.IsDeleted = true;
                return await _genericRepository.Update(Entity);
            }
        }
        public async Task<IEnumerable<UserRoleVM>> UserRolesAll()
        {
            var list = new List<UserRoleVM>();
            var temp = await _context.UserRoles.Where(p => p.IsDeleted != true).ToListAsync();
            if (temp != null && temp.Count > 0)
            {
                foreach (var item in temp)
                {
                    var model = new UserRoleVM()
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                    list.Add(model);
                }
            }
            return list;
        }
        #endregion
        #region Users
        public async Task<PagingModel<UserVM>> UserGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10)
        {
            return await _genericRepository.GetPagedData<UserVM>($@"[{DbSchema.Setup}].[{nameof(UserGetAll)}]", PageIndex, PageSize, SearchTerm, GetAllParms.NotDeleted, GetAllParms.TempId);
        }
        public async Task<User> UserEntity(int id)
        {
            return await _genericRepository.GetDataById<User>($@"[{DbSchema.Setup}].[{nameof(UserGetAll)}]", GetAllParms.PageIndex, GetAllParms.PageSize, GetAllParms.SearchTerm, GetAllParms.NotDeleted, id);
        }
        public async Task<bool> UserCreate(UserVM model)
        {
            var Entity = new User()
            {
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                RoleId = model.RoleId,
            };
            return await _genericRepository.Create(Entity);
        }
        public async Task<bool> UserUpdate(UserVM model)
        {
            var Entity = await UserEntity(model.Id);
            if (Entity == null)
            {
                return false;
            }
            else
            {
                Entity.Name = model.Name;
                Entity.UpdatedDate = DateTime.Now;
                Entity.Email = model.Email;
                Entity.Password = model.Password;
                Entity.RoleId = model.RoleId;
                return await _genericRepository.Update(Entity);
            }
        }
        public async Task<bool> UserDelete(int id)
        {
            var Entity = await UserEntity(id);
            if (Entity == null)
            {
                return false;
            }
            else
            {
                Entity.IsDeleted = true;
                Entity.UpdatedDate = DateTime.Now;
                return await _genericRepository.Update(Entity);
            }
        }
        public async Task<UserVM> UserGetById(int id)
        {
            var Entity = await UserEntity(id);
            if (Entity == null)
            {
                return new UserVM();
            }
            else
            {
                return new UserVM()
                {
                    Id = Entity.Id,
                    Name = Entity.Name,
                    Email = Entity.Email,
                    Password = Entity.Password,
                    RoleId = Entity.RoleId,
                    Roles = await UserRolesAll()
                };
            }
        }
        public async Task<User> Login(LoginVM model)
        {
            return await _context.Users.Where(p => p.Email == model.Username && p.Password == model.Password).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<DashboardCountVM>> DashboardCount()
        {
            return await _genericRepository.GetAllData<DashboardCountVM>("[Lottery].[DashboardCount]");
        }
        #endregion

    }
}
