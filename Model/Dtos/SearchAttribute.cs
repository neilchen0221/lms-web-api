using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class SearchAttribute
    {
        public string SearchValue { get; set; }
        public string SortOrder { get; set; }
        public string SortString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
