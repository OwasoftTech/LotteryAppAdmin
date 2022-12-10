using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }
        public string CountryCode { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
