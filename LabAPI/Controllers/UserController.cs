using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaboratoryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public ActionResult Post([FromBody] UserRequest userCredential)
        {
            var token = _repo.CreateUser(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        /// <summary>
        /// Authenticate users
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserRequest userCredential)
        {
            var token = _repo.AuthenticateUser(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
