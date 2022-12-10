using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
