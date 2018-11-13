using BL.Managers.Interfaces;
using Data.Database;
using Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    public class TestsController : ApiController
    {
        private readonly IUserManager _userManager;
        private readonly IStudentManager _studentManager;

        public TestsController(IUserManager userManager,IStudentManager studentManager)
        {
            _userManager = userManager;
            _studentManager = studentManager;
        }

        [HttpPost]
        [Route("api/test/createuser")]
        public IHttpActionResult Post(UserRegisterDto user)
        {
            var userDisplay = _userManager.CreateUser(user);
            return Ok(userDisplay);
        }

        [HttpGet]
        [Route("api/test/search")]
        public IHttpActionResult Get(string sortString = "id", string sortOrder = "asc", string searchValue = "", int pageSize = 10, int pageNumber = 1)
        {
            SearchAttribute search = new SearchAttribute()
            {
                SearchValue = searchValue,
                SortOrder = sortOrder,
                SortString = sortString,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            StudentSearchDto students = _studentManager.Search(search);
            return Ok(students);
        }



        [HttpGet]
        [Route("api/test/students")]
        public IHttpActionResult Test()
        {
            var students = _studentManager.GetAll();
                return Ok(students);
            
        }
    }
}
