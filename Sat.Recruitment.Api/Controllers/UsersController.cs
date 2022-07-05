using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Domain;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("/create-user")]
        public IActionResult CreateUser([FromBody] User userRequest)
        {
            return Ok(_userService.CreateUser(userRequest));
        }
    }
}
