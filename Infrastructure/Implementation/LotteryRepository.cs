using DbModels;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
using Infrastructure.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class LotteryRepository : ILotteryRepository
    {
        private IGenericRepository _genericRepository;
        private LotteryAppDBContext _context;
        private string _ConnectionString;

        public LotteryRepository(IGenericRepository genericRepository, LotteryAppDBContext context, IConfiguration config)
        {
            _genericRepository = genericRepository;
            _context = context;
            _ConnectionString = config.GetConnectionString("LotteryAppDatabase");
        }
        #region Lottery
        public async Task<PagingModel<LotteryVM>> LotteryGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10)
        {
            return await _genericRepository.GetPagedData<LotteryVM>($@"[{DbSchema.Lottery}].[{nameof(LotteryGetAll)}]", PageIndex, PageSize, SearchTerm, GetAllParms.NotDeleted, GetAllParms.TempId);
        }
        public async Task<IEnumerable<LotteryVM>> AppLotteryGetAll()
        {
            return await _genericRepository.GetAllData<LotteryVM>($@"[{DbSchema.App}].[{nameof(LotteryGetAll)}]");
        }
        public async Task<Lottery> LotteryEntity(int id)
        {
            return await _genericRepository.GetDataById<Lottery>($@"[{DbSchema.Lottery}].[{nameof(LotteryGetAll)}]", GetAllParms.PageIndex, GetAllParms.PageSize, GetAllParms.SearchTerm, GetAllParms.NotDeleted, id);
        }
        public async Task<LotteryCreateVM> LotteryGetById(int id)
        {
            var Entity = await LotteryEntity(id);
            if (Entity == null)
            {
                return new LotteryCreateVM();
            }
            else
            {
                return new LotteryCreateVM()
                {
                    Id = Entity.Id,
                    Name = Entity.Name,
                    ExpiryDate = Entity.ExpiryDate,
                    LotteryImgName = Entity.LotteryImgName,
                    RewardPrice = Entity.RewardPrice,
                    TokenLimit = Entity.TokenLimit,
                    Description = Entity.Description,
                    IsRecursive = Entity.IsRecursive,
                };
            }
        }
        public async Task<bool> LotteryCreate(LotteryCreateVM model)
        {
            var Entity = new Lottery()
            {
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                Name = model.Name,
                ExpiryDate = model.ExpiryDate.Value,
                IsOpened = false,
                LotteryImgName = model.LotteryImgName,
                RewardPrice = model.RewardPrice,
                TokenLimit = model.TokenLimit,
                Description = model.Description,
                IsRecursive = model.IsRecursive
            };
            return await _genericRepository.Create(Entity);
        }
        public async Task<bool> LotteryUpdate(LotteryCreateVM model)
        {
            var Entity = await LotteryEntity(model.Id);
            if (Entity == null)
            {
                return false;
            }
            else
            {
                Entity.Name = model.Name;
                Entity.UpdatedDate = DateTime.Now;
                //Entity.ExpiryDate = model.ExpiryDate;
                Entity.RewardPrice = model.RewardPrice;
                Entity.TokenLimit = model.TokenLimit;
                Entity.Description = model.Description;
                Entity.IsRecursive = model.IsRecursive;
                if (model.LotteryImgName != null)
                {
                    Entity.LotteryImgName = model.LotteryImgName;
                }
                return await _genericRepository.Update(Entity);
            }
        }
        public async Task<bool> LotteryDelete(int id)
        {
            var Entity = await LotteryEntity(id);
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
        #endregion
        public async Task<CandidateCount> CandidateCount(int LotteryId)
        {
            var Data = new CandidateCount();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Lottery].[CandidateCount]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LotteryId", LotteryId);
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToSingle<CandidateCount>(reader);
                        Data = BindingResult;
                    }
                    return Data;
                }
            }
            return Data;
        }
        public async Task<bool> LotteryCandidateCreate(LotteryCandidate Entity)
        {
            return await _genericRepository.Create(Entity);
        }
        public async Task<bool> LotteryWinerCreate(LotteryWiner Entity)
        {
            return await _genericRepository.Create(Entity);
        }
        public async Task<IEnumerable<LotteryCandidate>> LotteryCandidateList(int LotteryId)
        {
            var response = new List<LotteryCandidate>();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[Lottery].[LotteryCandidateList]", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LotteryId", LotteryId);
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToList<LotteryCandidate>(reader);
                        response = BindingResult;
                    }
                    return response;
                }
            }
        }
        public async Task<int> LotteryCandidateAnnonce(int LotteryId)
        {
            int Data = 0;
            using (SqlConnection conn = new SqlConnection(_ConnectionString))
            using (SqlCommand cmd = new SqlCommand("[Lottery].[LotteryCandidateAnnonce]", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LotteryId", LotteryId);
                // @ReturnVal could be any name
                var returnParameter = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                Data = (int)returnParameter.Value;
            }
            return Data;

        }
        public async Task<IEnumerable<LotteryWinerVM>> AppLotteryWinnerGetAll()
        {
            return await _genericRepository.GetAllData<LotteryWinerVM>($@"[App].[LotteryWinnerGetAll]");
        }
        public async Task<PagingModel<LotteryWinerVM>> LotteryWinnerLotteryGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10)
        {
            return await _genericRepository.GetPagedData<LotteryWinerVM>($@"[Lottery].[LotteryWinnerGetAll]", PageIndex, PageSize, SearchTerm, GetAllParms.NotDeleted, GetAllParms.TempId);
        }
    }
}
