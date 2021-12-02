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

        [HttpGet("ConferenceTypes")]
        public async Task<ActionResult> GetConferenceTypes()
        {
            var types = await _conferencesService.GetConferenceTypes();

            return Ok(types);
        }

        [HttpGet("ConferenceSubTypes")]
        public async Task<ActionResult> GetConferenceSubTypes()
        {
            var types = await _conferencesService.GetConferenceSubTypes();

            return Ok(types);
        }

        [HttpGet("Conferences")]
        public async Task<ActionResult> GetConferences([FromQuery] int? typeValue, [FromQuery] int? subTypeValue)
        {
            var conferences = await _conferencesService.GetConferences(typeValue, subTypeValue);

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
