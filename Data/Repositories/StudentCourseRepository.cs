using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Data.Repositories.Interfaces;
using Data.Database;

namespace Data.Repositories
{
    public class StudentCourseRepository : GenericRepository<StudentCourse>, IStudentCourseRepository
    {
        public StudentCourseRepository(LMSEntities context) : base(context)
        {
        }
    }
}
