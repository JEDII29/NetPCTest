using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetPCTest.Domain.Abstract;
using NetPCTest.Data.Entities;
using NetPCTest.API.Requests;

namespace NetPCTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest)
        {
            if (_authenticationService.IsValidUser(loginRequest.Username, loginRequest.Password))
            {
                var token = await _authenticationService.GenerateAccessToken(loginRequest.Username);
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
