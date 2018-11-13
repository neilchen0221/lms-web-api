using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace BL.Managers.Interfaces
{
    public interface ICourseManager
    {
        Course GetCourseById(int id);
        IEnumerable<Course> GetAll();
        bool DeleteCourse(int id);
        Course CreateCourse(Course course);
        Course Update(int id, Course course);
        bool Any(int id);
    }
}
