using System;
using System.Collections.Generic;

#nullable disable

namespace DbModels
{
    public partial class WebsiteSetting
    {
        public string SettingName { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
        public int TypeId { get; set; }
        public bool IsStaging { get; set; }
        public bool IsLive { get; set; }
        public bool IsAcceptance { get; set; }
    }
}
