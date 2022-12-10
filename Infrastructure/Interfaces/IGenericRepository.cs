using Infrastructure.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository
    {
        Task<bool> Create(object Entity);
        Task<bool> Update(object Entity);
        Task<PagingModel<T>> GetPagedData<T>(string StoreProcedureName, int PageIndex = 1, int PageSize = 10, string SearchTerm = "", int IsDelted = 0, int Id = 0) where T : new();
        Task<List<T>> GetAllData<T>(string StoreProcedureName) where T : new();
        Task<T> GetDataById<T>(string StoreProcedureName, int PageIndex = 1, int PageSize = 10, string SearchTerm = "", int IsDelted = 0, int Id = 0) where T : new();
    }
}
