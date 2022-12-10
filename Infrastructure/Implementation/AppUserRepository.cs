using DbModels;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using Infrastructure.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class AppUserRepository : IAppUserRepository
    {
        private IGenericRepository _genericRepository;
        private string _ConnectionString;

        public AppUserRepository(IGenericRepository genericRepository , IConfiguration config)
        {
            _genericRepository = genericRepository;
            _ConnectionString = config.GetConnectionString("LotteryAppDatabase");
        }
        public async Task<bool> AppUserSignUp(AppUserSignUpVM model)
        {
            var Entity = new AppUser
            {
                CountryCode = model.CountryCode,
                IsDeleted = false,
                Name = model.Name,
                Phone = model.Phone,
                CreatedDate = DateTime.Now
            };
            return await _genericRepository.Create(Entity);
        }
        public async Task<AppUser> AppUserEntity(int id)
        {
            return await _genericRepository.GetDataById<AppUser>($@"[{DbSchema.App}].[AppUserGetAll]", GetAllParms.PageIndex, GetAllParms.PageSize, GetAllParms.SearchTerm, GetAllParms.NotDeleted, id);
        }
        public async Task<bool> AppUserUpdate(AppUserUpdateVM model)
        {
            var Entity = await AppUserEntity(model.Id);
            if(Entity!= null)
            {
                Entity.Name = model.Name;
                Entity.PhotoName = model.photoName;
                return await _genericRepository.Update(Entity); 
            }
            else
            {
                return false;
            }           
        }
        public async Task<AppUserVM> AppUserSignIn(AppUserSignin model)
        {
            var Data = new AppUserVM();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"[App].[AppUserSignIn]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryCode", model.CountryCode);
                    cmd.Parameters.AddWithValue("@Phone", model.Phone);
                    cmd.Parameters.AddWithValue("@Id", 0);
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToSingle<AppUserVM>(reader);
                        Data = BindingResult;
                    }
                    return Data;
                }
            }
            return Data;
        }
        public async Task<AppUserVM> AppUserGetById(int id)
        {
            var Data = new AppUserVM();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($@"[App].[AppUserSignIn]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryCode", "");
                    cmd.Parameters.AddWithValue("@Phone", "");
                    cmd.Parameters.AddWithValue("@Id", id);
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToSingle<AppUserVM>(reader);
                        Data = BindingResult;
                    }
                    return Data;
                }
            }
            return Data;
        }
        public async Task<PagingModel<AppUserVM>> AppUsersGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10)
        {
            return await _genericRepository.GetPagedData<AppUserVM>($@"[{DbSchema.App}].[AppUserGetAll]", PageIndex, PageSize, SearchTerm, GetAllParms.NotDeleted, GetAllParms.TempId);
        }
    }
}
