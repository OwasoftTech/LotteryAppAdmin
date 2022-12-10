using DbModels;
using Infrastructure.Interfaces;
using Infrastructure.Utilities;
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
    public class GenericRepository : IGenericRepository
    {
        private LotteryAppDBContext _context;
        private string _ConnectionString;

        public GenericRepository(LotteryAppDBContext context , IConfiguration config)
        {
            _context = context;
            _ConnectionString = config.GetConnectionString("LotteryAppDatabase");
        }
        public async Task<bool> Create(object Entity)
        {
            _context.Add(Entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Update(object Entity)
        {
            _context.Update(Entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<PagingModel<T>> GetPagedData<T>(string StoreProcedureName, int PageIndex = 1, int PageSize = 10, string SearchTerm = "", int IsDelted = 0 , int Id =0) where T : new()
        {
            var response = new PagingModel<T>();
            response.SearchTerm = SearchTerm;
            response.PageIndex = PageIndex;
            response.PageSize = PageSize;
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoreProcedureName, sql))
                {              
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsDeleted", IsDelted);
                    cmd.Parameters.AddWithValue("@SearchTerm", SearchTerm);
                    cmd.Parameters.AddWithValue("@PageIndex", PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.VarChar, 30);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToList<T>(reader);
                        response.Data = BindingResult;
                    }
                    response.RecordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                    return response;
                }
            }
        }
        public async Task<List<T>> GetAllData<T>(string StoreProcedureName) where T : new()
        {
            var response = new List<T>();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoreProcedureName, sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToList<T>(reader);
                        response = BindingResult;
                    }
                    return response;
                }
            }
        }
        public async Task<T> GetDataById<T>(string StoreProcedureName, int PageIndex = 1, int PageSize = 10, string SearchTerm = "", int IsDelted = 0, int Id = 0) where T : new()
        {
            var Data = new T();
            using (SqlConnection sql = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(StoreProcedureName, sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsDeleted", IsDelted);
                    cmd.Parameters.AddWithValue("@SearchTerm", SearchTerm);
                    cmd.Parameters.AddWithValue("@PageIndex", PageIndex);
                    cmd.Parameters.AddWithValue("@PageSize", PageSize);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.Add("@RecordCount", SqlDbType.VarChar, 30);
                    cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var BindingResult = DataMapperExtensions.MapToSingle<T>(reader);
                        Data = BindingResult;
                    }
                    return Data;
                }
            }
        }

    }
}
