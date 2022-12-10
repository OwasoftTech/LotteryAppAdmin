using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class LotteryCandidate
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int LotteryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
