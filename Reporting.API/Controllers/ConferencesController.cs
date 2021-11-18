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
    public class ConferencesController : ControllerBase
    {
        private readonly IConferencesService _conferencesService;

        public ConferencesController(IConferencesService conferencesService)
        {
            _conferencesService = conferencesService;
        }

        [HttpGet("Conferences")]
        public async Task<ActionResult> GetConferences()
        {
            var conferences = await _conferencesService.GetConferences();

            return Ok(conferences);
        }

        [HttpPost("Conferences")]
        public async Task<ActionResult> CreateConference([FromBody] CreateConferenceDto dto)
        {
            var conference = await _conferencesService.CreateConference(dto);

            return Ok(conference);
        }

        [HttpPut("Conferences/{id}")]
        public async Task<ActionResult> UpdateConference(int id, [FromBody] CreateConferenceDto dto)
        {
            var conference = await _conferencesService.UpdateConference(id, dto);

            return Ok(conference);
        }

        [HttpDelete("Conferences/{id}")]
        public async Task<ActionResult> DeleteConference(int id)
        {
            await _conferencesService.DeleteConference(id);

            return Ok();
        }
    }
}
