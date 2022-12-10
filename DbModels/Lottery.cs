using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class Lottery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal RewardPrice { get; set; }
        public int TokenLimit { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsOpened { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string LotteryImgName { get; set; }
        public string Description { get; set; }
    }
}
