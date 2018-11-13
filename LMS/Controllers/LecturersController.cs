using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.Models;
using Model.Dtos;
using BL.Managers.Interfaces;

namespace LMS.Controllers
{
    public class LecturersController : ApiController
    {
        private ILecturerManager _lecturerManager;
        private ICourseManager _courseManager;

        public LecturersController(ILecturerManager lecturerManager, ICourseManager courseManager)
        {
            _lecturerManager = lecturerManager;
            _courseManager = courseManager;
        }


        // GET: api/Lecturers
        [HttpGet]
        [Route("api/Lecturers")]
        public IHttpActionResult GetAll()
        {
            return Ok(_lecturerManager.GetAll());
        }

        // GET: api/Lecturers/5
        [HttpGet]
        [Route("api/Lecturers/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (_lecturerManager.Any(id))
            {
                return Ok(_lecturerManager.GetLecturerById(id));
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Lecturers
        [HttpPost]
        [Route("api/Lecturers")]
        public IHttpActionResult Post([FromBody]Lecturer lecturer)
        {
            try
            {
                return Ok(_lecturerManager.CreateLecturer(lecturer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Lecturers/5
        [HttpPut]
        [Route("api/Lecturers/{id}")]
        public IHttpActionResult Put(int id, [FromBody]Lecturer lecturer)
        {
            try
            {
                if (!_lecturerManager.Any(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_lecturerManager.Update(id, lecturer));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Lecturers/5
        [HttpDelete]
        [Route("api/Lecturers/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (_lecturerManager.Delete(id))
                {
                    return Ok($"Lecturer {id} deleted.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Lecturers/addToCourse")]
        public IHttpActionResult AddToCourse(int id, [FromBody]Course course)
        {
            try
            {
                if (_lecturerManager.Any(id) && _courseManager.Any(course.Id))
                {
                    return Ok(_lecturerManager.AddToCourse(id, course));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
