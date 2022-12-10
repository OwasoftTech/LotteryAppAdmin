using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class LotteryWiner
    {
        public int Id { get; set; }
        public int LotteryId { get; set; }
        public int WinerId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
