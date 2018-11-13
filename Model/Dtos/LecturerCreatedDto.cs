using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dtos
{
    public class LecturerCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StaffNumber { get; set; }
        public string Email { get; set; }
        public string Bibliography { get; set; }
        public bool IsAvailable { get; set; }
    }
}
