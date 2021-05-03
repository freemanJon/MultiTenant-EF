using System.Threading.Tasks;
using ApiMultiTenant.Data;
using ApiMultiTenant.Models;
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

        [HttpPost]
        public IActionResult Post([FromBody] Users user){
            if(user is null)
                return BadRequest();
            _userService.Add(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id){
            var user = _userService.GetById(id);
            if(user is null)
                return NotFound();
            _userService.Delete(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Users user){
            var userUpdate = _userService.GetById(id);
            if(userUpdate is null)
                return NotFound();
            userUpdate.name = user.name;
            _userService.Update(userUpdate);
            return Ok();
        }

    }
}
