using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Dtos;

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
        IEnumerable<StudentDto> GetCourseStudent(int courseId);
        IEnumerable<Lecturer> GetCourseLecturer(int courseId);
    }
}
