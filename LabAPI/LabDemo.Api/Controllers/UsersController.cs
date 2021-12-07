using LabDemo.Models;
using LabDemo.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;
        public UsersController(ILogger<UsersController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] Login user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid UserName Or Password");
            }

            var result = _userService.GetUser(user.UserName, user.Password);
            if (result != null)
            {
                var token = _userService.GenerateJWT(result);
                return Ok(token);
            }
            return NotFound(result);
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
              user.Role = "Admin";
            _userService.AddUser(user);

            return Ok();
        }
    }
}
