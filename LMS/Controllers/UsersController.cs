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
    public class UsersController : ApiController
    {
        private IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/users")]
        public IHttpActionResult CreateUser([FromBody]UserRegisterDto user)
        {
            try
            {
                return Ok(_userManager.CreateUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        public IHttpActionResult FindUser(string username, string password)
        {
            if (_userManager.FindUser(username, password) == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(_userManager.FindUser(username, password));
            }
        }
    }
}
