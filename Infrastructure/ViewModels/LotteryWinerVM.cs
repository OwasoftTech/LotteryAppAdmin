using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class LotteryWinerVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string AppUserName { get; set; }
        public decimal RewardPrice { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string ProfilePicUrl { get; set; }
        public string LotteryName { get; set; }
        public DateTime WinningDate { get; set; }
        public string LotteryImgUrl { get; set; }
    }
}
