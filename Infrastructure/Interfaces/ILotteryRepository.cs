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
    public interface ILotteryRepository
    {
        Task<PagingModel<LotteryVM>> LotteryGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
        Task<IEnumerable<LotteryVM>> AppLotteryGetAll();
        Task<LotteryCreateVM> LotteryGetById(int id);
        Task<bool> LotteryCreate(LotteryCreateVM model);
        Task<bool> LotteryUpdate(LotteryCreateVM model);
        Task<bool> LotteryDelete(int id);
        Task<CandidateCount> CandidateCount(int LotteryId);
        Task<bool> LotteryCandidateCreate(LotteryCandidate Entity);
        Task<bool> LotteryWinerCreate(LotteryWiner Entity);
        Task<IEnumerable<LotteryCandidate>> LotteryCandidateList(int LotteryId);
        Task<int> LotteryCandidateAnnonce(int LotteryId);
        Task<IEnumerable<LotteryWinerVM>> AppLotteryWinnerGetAll();
        Task<PagingModel<LotteryWinerVM>> LotteryWinnerLotteryGetAll(string SearchTerm = "", int PageIndex = 1, int PageSize = 10);
    }
}
