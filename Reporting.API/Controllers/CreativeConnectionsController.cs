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
    public class CreativeConnectionsController : ControllerBase
    {
        private readonly ICreativeConnectionsService _creativeConnectionsService;

        public CreativeConnectionsController(ICreativeConnectionsService creativeConnectionsService)
        {
            _creativeConnectionsService = creativeConnectionsService;
        }

        [HttpGet("CreativeConnectionTypes")]
        public async Task<ActionResult> GetCreativeConnectionTypes()
        {
            var types = await _creativeConnectionsService.GetCreativeConnectionTypes();

            return Ok(types);
        }

        [HttpGet("CreativeConnections")]
        public async Task<ActionResult> GetCreativeConnections()
        {
            var CreativeConnections = await _creativeConnectionsService.GetDepartmentCreativeConnections();

            return Ok(CreativeConnections);
        }

        [HttpPost("CreativeConnections")]
        public async Task<ActionResult> CreateCreativeConnection([FromBody] CreateCreativeConnectionDto dto)
        {
            await _creativeConnectionsService.CreateCreativeConnection(dto);

            return Ok();
        }

        [HttpPut("CreativeConnections/{id}")]
        public async Task<ActionResult> UpdateCreativeConnection(int id, [FromBody] CreateCreativeConnectionDto dto)
        {
            await _creativeConnectionsService.UpdateCreativeConnection(id, dto);

            return Ok();
        }

        [HttpDelete("CreativeConnections/{id}")]
        public async Task<ActionResult> DeleteCreativeConnection(int id)
        {
            await _creativeConnectionsService.DeleteCreativeConnection(id);

            return Ok();
        }
    }
}
