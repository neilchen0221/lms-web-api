using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class StudentSearchDto
    {
        public IEnumerable<StudentDto> Students { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
    }
}
