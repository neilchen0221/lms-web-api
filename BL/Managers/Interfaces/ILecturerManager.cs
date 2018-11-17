using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Model.Dtos;
namespace BL.Managers.Interfaces
{
    public interface ILecturerManager
    {
        Lecturer GetLecturerById(int id);
        IEnumerable<Lecturer> GetAll();
        bool Delete(int id);
        Lecturer Update(int id, Lecturer lecturer);
        LecturerCreatedDto CreateLecturer(Lecturer lecturer);
        bool Any(int id);
        LecturerCourse AddToCourse(int lecturerId, int courseId);
        void RemoveFromCourse(int lecturerId, int courseId);
        IEnumerable<CourseDisplayDto> GetLecturerCourse(int lecturerId);
    }
}
