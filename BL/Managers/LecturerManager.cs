using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
using Data.Repositories.Interfaces;
using BL.Managers.Interfaces;
using Model.Dtos;
using Data.Repositories;
using AutoMapper;
namespace BL.Managers
{
    public class LecturerManager : ILecturerManager
    {
        private ILecturerRepository _lecturerRepository;
        private ILecturerCourseRepository _lecturerCourseRepository;
        private ICourseRepository _courseRepository;

        public LecturerManager(ILecturerRepository lecturerRepository, ILecturerCourseRepository lecturerCourseRepository,ICourseRepository courseRepository)
        {
            _lecturerRepository = lecturerRepository;
            _lecturerCourseRepository = lecturerCourseRepository;
            _courseRepository = courseRepository;
        }

        public LecturerCreatedDto CreateLecturer(Lecturer lecturer)
        {
           if(_lecturerRepository.Records.Any(x=>x.Email == lecturer.Email || x.StaffNumber == lecturer.StaffNumber))
            {
                var createdLecturer = Mapper.Map<Lecturer, LecturerCreatedDto>(lecturer);
                createdLecturer.IsAvailable = false;
                return createdLecturer;
            }
            else
            {
                lecturer = _lecturerRepository.Add(lecturer);
                var createdLecturer = Mapper.Map<Lecturer, LecturerCreatedDto>(lecturer);
                createdLecturer.IsAvailable = true;
                return createdLecturer;
            }
        }

        public bool Delete(int id)
        {
            if (_lecturerRepository.Records.Any(x => x.Id == id))
            {
                var courses = _lecturerCourseRepository.Records.Where(x => x.CourseId == id).ToList();
                foreach (var course in courses)
                {
                    _lecturerCourseRepository.Delete(course);
                }
                _lecturerRepository.Delete(_lecturerRepository.GetById(id));
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Lecturer> GetAll()
        {
            return _lecturerRepository.GetAll();
        }

        public Lecturer GetLecturerById(int id)
        {
            return (_lecturerRepository.GetById(id) == null) ? null : _lecturerRepository.GetById(id);
        }

        public Lecturer Update(int id, Lecturer lecturer)
        {
            lecturer.Id = id;
            return _lecturerRepository.Update(lecturer);
        }

        public bool Any(int id)
        {
            return _lecturerRepository.Records.Any(x => x.Id == id);
        }

        public LecturerCourse AddToCourse(int lecturerId, int courseId)
        {
            var lecturerCourse = new LecturerCourse
            {
                CourseId = courseId,
                LecturerId = lecturerId
            };

            lecturerCourse = _lecturerCourseRepository.Add(lecturerCourse);
            return lecturerCourse;
        }

        public void RemoveFromCourse(int lecturerId, int courseId)
        {
            var lecturerCourse = _lecturerCourseRepository.Records.FirstOrDefault(x => x.LecturerId == lecturerId && x.CourseId == courseId);
            if (lecturerCourse != null)
            {
                _lecturerCourseRepository.Delete(lecturerCourse);
            }
        }

        public IEnumerable<CourseDisplayDto> GetLecturerCourse(int lecturerId)
        {
            var lecturerCourses = _lecturerCourseRepository.Records.Where(x => x.LecturerId == lecturerId).ToList();

            var courseList = new List<Course>();
            foreach (var lecturerCourse in lecturerCourses)
            {
                var course = _courseRepository.GetById(lecturerCourse.CourseId);
                courseList.Add(course);
            }

            var courseDiplayList = Mapper.Map<List<Course>, List<CourseDisplayDto>>(courseList);

            return courseDiplayList;
        }
    }
}
