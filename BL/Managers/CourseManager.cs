using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Managers.Interfaces;
using Model.Models;
using Data.Repositories.Interfaces;
namespace BL.Managers
{
    public class CourseManager : ICourseManager
    {
        private ICourseRepository _courseRepository;
        private ILecturerCourseRepository _lecturerCourseRepository;
        private IStudentCourseRepository _studentCourseRepository;
        private IStudentRepository _studentRepository;

        public CourseManager(ICourseRepository courseRepository, 
                             IStudentCourseRepository studentCourseRepository, 
                             ILecturerCourseRepository lecturerCourseRepository,
                             IStudentRepository studentRepository)
        {
            _courseRepository = courseRepository;
            _studentCourseRepository = studentCourseRepository;
            _lecturerCourseRepository = lecturerCourseRepository;
            _studentRepository = studentRepository;
        }

        public Course CreateCourse(Course course)
        {
            course = _courseRepository.Add(course);
            return course;
        }

        public bool DeleteCourse(int id)
        {
            if(_courseRepository.Records.Any(x=>x.Id == id))
            {
                _courseRepository.Delete(_courseRepository.GetById(id));
                var studentCourse = _studentCourseRepository.Records.Where(x => x.CourseId == id);
                foreach (var item in studentCourse)
                {
                    var student = _studentRepository.GetById(item.StudentId);
                    student.Credit += 4;
                    _studentRepository.Update(student);
                    _studentCourseRepository.Delete(item);
                }
                var lecturerCourse = _lecturerCourseRepository.Records.Where(x => x.CourseId == id);
                foreach (var item in lecturerCourse)
                {
                    _lecturerCourseRepository.Delete(item);

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Course> GetAll()
        {
            return _courseRepository.GetAll();
        }

        public Course GetCourseById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public Course Update(int id, Course course)
        {
            course.Id = id;
            return _courseRepository.Update(course);
        }

        public bool Any (int id)
        {
            return _courseRepository.Records.Any(x => x.Id == id);
        }
    }
}
