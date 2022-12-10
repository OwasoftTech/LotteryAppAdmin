using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public static class GetAllParms
    {
        public static int Deleted { get; set; } = 1;
        public static int NotDeleted { get; set; } = 0;
        public static int TempId { get; set; } = 0;
        public static string SearchTerm { get; set; } = "";
        public static int PageIndex { get; set; } = 1;
        public static int PageSize { get; set; } = 10;
    }
}
