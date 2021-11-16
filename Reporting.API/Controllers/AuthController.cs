using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;

namespace Reporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.Login(dto);

            if (token == null)
            {
                return Ok(new { message = "Username or password is incorrect" });
            }
            
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            var token = await _authService.Register(dto);

            return Ok(new { Token = token });
        }
    }
}
