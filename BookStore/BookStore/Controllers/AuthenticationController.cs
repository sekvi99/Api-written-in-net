using BookStore.Entities;
using BookStore.Models;
using BookStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {

            _authenticationService = authenticationService;
        }

        [HttpPost("api/register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto request)
        {
            await _authenticationService.Register(request);
            return Ok();
        }

        [HttpPost("api/login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto request)
        {
            var token = await _authenticationService.Login(request);
            return Ok(new { Token = token });
        }
    }
}
