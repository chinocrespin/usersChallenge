using Core.Common.Presentation;
using Identity.Domain.IServices;
using Identity.Domain.Models;
using Identity.Domain.Queries;
using Microsoft.AspNetCore.Mvc;

namespace UsersChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public Result<User> Get([FromQuery] UsersQuery query)
        {
            return _usersService.GetAll(query);
        }
        
        [HttpGet("{id}")]
        public User Get(string id)
        {
            return _usersService.GetById(id);
        }
        
        [HttpPut]
        public bool Put([FromBody] User user)
        {
            return _usersService.Update(user);
        }
        
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _usersService.Delete(id);
        }
    }
}
