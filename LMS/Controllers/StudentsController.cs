using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL.Managers.Interfaces;
using Model.Models;
using Model.Dtos;

namespace LMS.Controllers
{
    [Authorize]
    public class StudentsController : ApiController
    {
        private IStudentManager _studentManager;
        private ICourseManager _courseManager;

        public StudentsController(IStudentManager studentManager, ICourseManager courseManager)
        {
            _studentManager = studentManager;
            _courseManager = courseManager;
        }

        //// GET: api/Students
        //[HttpGet]
        //[Route("api/students")]
        //public IHttpActionResult GetAll()
        //{
        //    return Ok(_studentManager.GetAll());
        //}

        [HttpGet]
        [Route("api/students")]
        public IHttpActionResult GetStudents(string sortString = "id", string sortOrder = "asc", string searchValue = "", int pageSize = 10, int pageNumber = 1)
        {
            var searchAttr = new SearchAttribute()
            {
                SearchValue = searchValue,
                SortOrder = sortOrder,
                SortString = sortString,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Ok(_studentManager.Search(searchAttr));
        }

        // GET: api/Students/5
        [HttpGet]
        [Route("api/students/{id}")]
        public IHttpActionResult GetById(int id)
        {
            if (_studentManager.GetStudentById(id) != null)
            {
                return Ok(_studentManager.GetStudentById(id));
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Students
        [HttpPost]
        [Route("api/students")]
        public IHttpActionResult Post([FromBody]Student student)
        {
            if (_studentManager.EmailDuplicate(student))
            {
                return BadRequest("Email already exists");
            }
            else
            {
                return Ok(_studentManager.CreateStudent(student));
            }

        }


        // PUT: api/Students/5
        [HttpPut]
        [Route("api/students/{id}")]
        public IHttpActionResult Put(int id, [FromBody]Student student)
        {
            try
            {
                if (!_studentManager.Any(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_studentManager.Update(id, student));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Students/5
        [HttpDelete]
        [Route("api/students/{id}")]
        public IHttpActionResult Delete(int id)
        {
            if (_studentManager.Delete(id))
            {
                return Ok($"Student {id} deleted.");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("api/students/enrollcourse")]
        public IHttpActionResult AddToCourse(int studentId, int courseId)
        {
            try
            {
                if (!_studentManager.Any(studentId))
                {
                    return BadRequest("Student Not Found.");
                }
                else if (!_courseManager.Any(courseId))
                {
                    return BadRequest("Course Not Found.");
                }
                else if (!_studentManager.StudentCanEnroll(studentId) || !_studentManager.CourseCanTakeStudent(courseId))
                {
                    string message = "";
                    if (!_studentManager.StudentCanEnroll(studentId))
                    {
                        message += $"Student does not have enough credits.{Environment.NewLine}";
                    }
                    if (!_studentManager.CourseCanTakeStudent(courseId))
                    {
                        message += "Course has reach the max student limit.";
                    }
                    return BadRequest(message);
                }
                else
                {
                    return Ok(_studentManager.AddToCourse(studentId, courseId));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/students/cancelcourse")]
        public IHttpActionResult CancelCourse(int studentId, int courseId)
        {
            _studentManager.CancelCourse(studentId, courseId);
            return Ok();
        }

        [HttpGet]
        [Route("api/studentcourse/{studentId}")]
        public IHttpActionResult getStudentCourse(int studentId)
        {
            return Ok(_studentManager.getStudentCourse(studentId));
        }
    }
}
