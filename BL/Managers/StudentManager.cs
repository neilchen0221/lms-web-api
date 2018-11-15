using AutoMapper;
using BL.Managers.Interfaces;
using Data.Repositories.Interfaces;
using Model.Dtos;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class StudentManager : IStudentManager
    {
        private IStudentRepository _studentRepository;
        private IStudentCourseRepository _studentCourseRepository;
        private ICourseRepository _courseRepository;
        public StudentManager(IStudentRepository studentRepository, IStudentCourseRepository studentCourseRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _studentCourseRepository = studentCourseRepository;
            _courseRepository = courseRepository;
        }

        public StudentCreatedDto CreateStudent(Student student)
        {
            student = _studentRepository.Add(student);
            var createdStudent = Mapper.Map<Student, StudentCreatedDto>(student);
            return createdStudent;
        }

        public StudentDto GetStudentById(int id)
        {
            return (_studentRepository.GetById(id) == null) ? null : Mapper.Map<Student, StudentDto>(_studentRepository.GetById(id));
        }

        public IEnumerable<StudentDto> GetAll()
        {
            return Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(_studentRepository.GetAll());
        }

        public bool Delete(int id)
        {
            if (_studentRepository.Records.Any(x => x.Id == id))
            {
                _studentRepository.Delete(_studentRepository.GetById(id));
                var courses = _studentCourseRepository.Records.Where(x => x.StudentId == id);
                foreach (var course in courses)
                {
                    _studentCourseRepository.Delete(course);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public StudentDto Update(int id, Student student)
        {
            student.Id = id;
            return Mapper.Map<Student, StudentDto>(_studentRepository.Update(student));
        }

        public bool Any(int id)
        {
            return _studentRepository.Records.Any(x => x.Id == id);
        }

        public StudentSearchDto Search(SearchAttribute search)
        {

            IEnumerable<Student> students = _studentRepository.Records.Where(x => x.FirstName.Contains(search.SearchValue)
                                                || x.LastName.Contains(search.SearchValue)
                                                || x.Email.Contains(search.SearchValue));

            int studentCount = students.Count();

            switch (search.SortString)
            {
                case "id":
                    students = (search.SortOrder.ToLower() == "desc") ? students.OrderByDescending(x => x.Id) : students.OrderBy(x => x.Id);
                    break;
                case "firstName":
                    students = (search.SortOrder.ToLower() == "desc") ? students.OrderByDescending(x => x.FirstName) : students.OrderBy(x => x.FirstName);
                    break;
                case "lastName":
                    students = (search.SortOrder.ToLower() == "desc") ? students.OrderByDescending(x => x.LastName) : students.OrderBy(x => x.LastName);
                    break;
                case "dateOfBirth":
                    students = (search.SortOrder.ToLower() == "desc") ? students.OrderByDescending(x => x.DateOfBirth) : students.OrderBy(x => x.DateOfBirth);
                    break;
                case "email":
                    students = (search.SortOrder.ToLower() == "desc") ? students.OrderByDescending(x => x.Email) : students.OrderBy(x => x.Email);
                    break;
                default:
                    students = students.OrderBy(x => x.Id);
                    break;
            }

            students = students.Skip((search.PageNumber - 1) * search.PageSize).Take(search.PageSize);

           

            var result = new StudentSearchDto()
            {
                Students =  Mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(students),
                PageNumber = search.PageNumber,
                TotalPage = studentCount / search.PageSize + (studentCount % search.PageSize == 0 ? 0 : 1),
                PageSize = search.PageSize
            };
            return result;
        }

        public StudentCourse AddToCourse(int studentId, int courseId)
        {
            var student = _studentRepository.GetById(studentId);
            student.Credit -= 4;
            _studentRepository.Update(student);

            var studentCourse = new StudentCourse
            {
                CourseId = courseId,
                StudentId = studentId
            };

            studentCourse = _studentCourseRepository.Add(studentCourse);
            return studentCourse;
        }

        public bool StudentCanEnroll(int studentId)
        {
            var credit = _studentRepository.GetById(studentId).Credit;
            return credit >= 4;
        }

        public bool CourseCanTakeStudent(int courseId)
        {
            var count = _studentCourseRepository.Records.Where(x => x.CourseId == courseId).Count();
            var maxStudent = _courseRepository.GetById(courseId).MaxStudent;
            return count < maxStudent;
        }

        public bool EmailDuplicate(Student student)
        {
            return _studentRepository.Records.Any(x => x.Email == student.Email);
        }

        public void CancelCourse(int studentId, int courseId)
        {
            var studentCourse = _studentCourseRepository.Records.FirstOrDefault(x => x.StudentId == studentId && x.CourseId == courseId);

            if (studentCourse != null)
            {
                var student = _studentRepository.GetById(studentId);
                student.Credit += 4;
                _studentRepository.Update(student);
                _studentCourseRepository.Delete(studentCourse);
            }
        }
    }
}

