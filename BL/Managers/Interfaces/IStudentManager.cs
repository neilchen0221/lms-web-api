﻿using Model.Dtos;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IStudentManager
    {
        StudentCreatedDto CreateStudent(Student student);
        StudentDto GetStudentById(int id);
        IEnumerable<StudentDto> GetAll();
        StudentSearchDto Search(SearchAttribute search);
        bool Delete(int id);
        StudentDto Update(int id, Student student);
        bool Any(int id);
        bool EmailDuplicate(Student student);
        StudentCourse AddToCourse(int studentId, int courseId );
        bool StudentCanEnroll(int studentId);
        bool CourseCanTakeStudent(int courseId);
        void CancelCourse(int studentId, int courseId);
        IEnumerable<CourseDisplayDto> GetStudentCourse(int studentId);
    }
}
