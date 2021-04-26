using System.Threading.Tasks;
using ApiMultiTenant.Data;
using ApiMultiTenant.Service;
using Microsoft.AspNetCore.Mvc;

namespace ApiMultiTenant.Controllers
{
    [Route("{clientName}/api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);

        }
    }
}