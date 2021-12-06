using LaboratoryAPI.Data.Interface;
using LaboratoryAPI.Data.Model;
using LaboratoryAPI.Data.Model.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserRepository _db;
        public UserController(IUserRepository userRepository)
        {
            _db = userRepository;
        }

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public ActionResult Post([FromBody] UserRequest userCredential)
        {
            try
            {
                string userCreated = _db.CreateUser(userCredential.UserName, userCredential.Password);
                return Ok(userCreated);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           
        }

        /// <summary>
        /// Authenticate users
        /// </summary>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authentication([FromBody] UserRequest userCredential)
        {
            var token = _db.AuthenticateUser(userCredential.UserName, userCredential.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
