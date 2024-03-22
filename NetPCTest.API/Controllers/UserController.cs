using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.Domain.Abstract;
using NetPCTest.Domain.Model;

namespace NetPCTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _contacService;
        public UserController(IUserService contactservice)
        {
            _contacService = contactservice;
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers() 
        {
            var users = await _contacService.GetUsersModels();
            return Ok(users);
        }
    }
}
