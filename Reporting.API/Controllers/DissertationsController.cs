using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;

namespace Reporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DissertationsController : ControllerBase
    {
        private readonly IDissertationsService _dissertationsService;

        public DissertationsController(IDissertationsService dissertationsService)
        {
            _dissertationsService = dissertationsService;
        }

        [HttpGet("UserDissertations")]
        public async Task<ActionResult> GetUserDissertations()
        {
            var dissertations = await _dissertationsService.GetUserDissertations();

            return Ok(dissertations);
        }

        [HttpGet("DepartmentDissertations")]
        public async Task<ActionResult> GetDepartmentDissertations()
        {
            var dissertations = await _dissertationsService.GetDepartmentDissertations();

            return Ok(dissertations);
        }

        [HttpPost("Dissertations")]
        public async Task<ActionResult> CreateDissertation([FromBody] CreateDissertationDto dto)
        {
            await _dissertationsService.CreateDissertation(dto);

            return Ok();
        }

        [HttpPut("Dissertations/{id}")]
        public async Task<ActionResult> UpdateDissertation(int id, [FromBody] CreateDissertationDto dto)
        {
            await _dissertationsService.UpdateDissertation(id, dto);

            return Ok();
        }

        [HttpDelete("Dissertations/{id}")]
        public async Task<ActionResult> DeleteDissertation(int id)
        {
            await _dissertationsService.DeleteDissertation(id);

            return Ok();
        }
    }
}
