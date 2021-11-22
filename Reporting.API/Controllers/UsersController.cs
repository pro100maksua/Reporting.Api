using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;

namespace Reporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IUsersService usersService, ICurrentUserService currentUserService)
        {
            _usersService = usersService;
            _currentUserService = currentUserService;
        }

        [HttpGet("Roles")]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _usersService.GetRoles();

            return Ok(roles);
        }

        [HttpGet("Faculties")]
        public async Task<ActionResult> GetFaculties()
        {
            var faculties = await _usersService.GetFaculties();

            return Ok(faculties);
        }

        [HttpGet("Departments")]
        public async Task<ActionResult> GetDepartments([FromQuery] int facultyValue)
        {
            var departments = await _usersService.GetDepartments(facultyValue);

            return Ok(departments);
        }

        [HttpPut("Users/{id}/IeeeXploreAuthorName")]
        public async Task<ActionResult> UpdateUserIeeeXploreAuthorName(int id, [FromBody] ValueDto<string> dto)
        {
            await _usersService.UpdateUserIeeeXploreAuthorName(id, dto.Value);

            return Ok();
        }
    }
}
