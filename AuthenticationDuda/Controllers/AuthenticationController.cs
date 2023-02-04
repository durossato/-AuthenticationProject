using AuthenticationDuda.Interfaces.Workers;
using AuthenticationDuda.Models;
using AuthenticationDuda.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace AuthenticationDuda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationWorker _authenticationWorker;

        public AuthenticationController(IAuthenticationWorker authenticationWorker)
        {
            _authenticationWorker = authenticationWorker;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            if (_authenticationWorker.Register(registerDto))
                return Ok();
            else
                throw new Exception("Erro interno");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            return Ok(_authenticationWorker.Login(loginDto));
        }
    }
}
