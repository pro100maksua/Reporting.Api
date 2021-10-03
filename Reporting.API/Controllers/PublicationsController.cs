using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;

namespace Reporting.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationsService _publicationsService;

        public PublicationsController(IPublicationsService publicationsService)
        {
            _publicationsService = publicationsService;
        }

        [HttpGet("PublicationTypes")]
        public async Task<ActionResult> GetPublicationTypes()
        {
            var types = await _publicationsService.GetPublicationTypes();

            return Ok(types);
        }

        [HttpPost("Publications")]
        public async Task<ActionResult> CreatePublication([FromBody] CreatePublicationDto dto)
        {
            var publication = await _publicationsService.CreatePublication(dto);

            return Ok(publication);
        }

        [HttpGet("ScopusArticles")]
        public async Task<ActionResult> GetPublicationFromScopus([FromQuery] string articleNumber, [FromQuery] string title)
        {
            var publication = await _publicationsService.GetPublicationFromScopus(articleNumber, title);

            if (publication == null)
            {
                return BadRequest(new { message = "Статтю не знайдено." });
            }

            return Ok(publication);
        }
    }
}
