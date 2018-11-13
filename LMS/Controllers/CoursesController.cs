using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Model.Models;
using BL.Managers.Interfaces;

namespace LMS.Controllers
{
    public class CoursesController : ApiController
    {
        private ICourseManager _courseManager;
        public CoursesController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }

        // GET: api/Courses
        [HttpGet]
        [Route("api/Courses")]
        public IHttpActionResult GetAll()
        {
            return Ok(_courseManager.GetAll());
        }

        // GET: api/Courses/5
        [HttpGet]
        [Route("api/Courses/{id}")]
        public IHttpActionResult Get(int id)
        {
            if (_courseManager.Any(id))
            {
                return Ok(_courseManager.GetCourseById(id));
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Courses
        [HttpPost]
        [Route("api/Courses")]
        public IHttpActionResult Post([FromBody]Course course)
        {
            try
            {
                return Ok(_courseManager.CreateCourse(course));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Courses/5
        [HttpPut]
        [Route("api/Courses/{id}")]
        public IHttpActionResult Put(int id, [FromBody]Course course)
        {
            try
            {
                if (_courseManager.Any(id))
                {
                    return Ok(_courseManager.Update(id, course));
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

        // DELETE: api/Courses/5
        [HttpDelete]
        [Route("api/Courses/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                if (_courseManager.DeleteCourse(id))
                {
                    return Ok($"Course {id} deleted.");
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
