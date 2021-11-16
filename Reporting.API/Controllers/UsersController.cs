using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;

namespace Reporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
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
    }
}
