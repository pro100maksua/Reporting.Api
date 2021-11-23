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
                return BadRequest(new { message = "Пошта або пароль неправильні" });
            }

            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto dto)
        {
            var response = await _authService.Register(dto);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(new { message = response.ErrorMessage });
            }

            return Ok(new { Token = response.Value });
        }

        [HttpPost("ValidateEmail")]
        public async Task<IActionResult> ValidateEmail([FromBody] ValidateValueDto dto)
        {
            var errorMessage = await _authService.ValidateEmail(dto);

            return Ok(new { message = errorMessage });
        }

        [HttpPut("LoggedInUser")]
        public async Task<ActionResult> UpdateLoggedInUser([FromBody] RegisterDto dto)
        {
            var response = await _authService.UpdateLoggedInUser(dto);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(new { message = response.ErrorMessage });
            }

            return Ok(new { Token = response.Value });
        }

        [HttpGet("LoggedInUser")]
        public async Task<ActionResult> GetLoggedInUser()
        {
            var user = await _authService.GetLoggedInUser();

            return Ok(user);
        }
    }
}
