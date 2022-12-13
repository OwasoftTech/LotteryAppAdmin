using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ViewModels
{
    public class LotteryVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal RewardPrice { get; set; }
        public int TokenLimit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsOpened { get; set; }
        public string LotteryImgName { get; set; } = null!;
        public string LotteryImgPath { get; set; }
        public string Description { get; set; } = null!;
        public int TotalApplied { get; set; }
        public bool IsRecursive { get; set; }
    }
    public class LotteryCreateVM
    {
        public int Id { get; set; }
        [Required( ErrorMessage = "Lottery Name is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Reward Price is required!")]
        [Range(1,int.MaxValue , ErrorMessage ="Reward Price must be greater then 0!")]
        public decimal RewardPrice { get; set; }
        [Required(ErrorMessage = "Token Limit is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Token Limit must be greater then 0!")]
        public int TokenLimit { get; set; }
        //[Required(ErrorMessage = "Lottery Exipry is required!")]
        public DateTime? ExpiryDate { get; set; }
        //[Required(ErrorMessage = "Lottery Image is required!")]
        public IFormFile? LotteryImg { get; set; } = null;
        public IFormFile? TempLotteryImg { get; set; } = null;
        public string? LotteryImgName { get; set; } = null;
        [Required(ErrorMessage = "Lottery Description is required!")]
        public string? Description { get; set; } = null;
        public bool IsRecursive { get; set; }
    }
    public class EnroleLotteryVM
    {
        [Range(0,int.MaxValue)]
        public int AppUserId { get; set; }
        [Range(0, int.MaxValue)]
        public int LotteryId { get; set; }
    }
    public class CandidateCount
    {
        public int Difference { get; set; }
    }
}
