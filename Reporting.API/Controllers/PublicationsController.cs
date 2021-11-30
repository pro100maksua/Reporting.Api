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
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationsService _publicationsService;
        private readonly ICurrentUserService _currentUserService;

        public PublicationsController(IPublicationsService publicationsService, ICurrentUserService currentUserService)
        {
            _publicationsService = publicationsService;
            _currentUserService = currentUserService;
        }

        [HttpGet("PublicationTypes")]
        public async Task<ActionResult> GetPublicationTypes()
        {
            var types = await _publicationsService.GetPublicationTypes();

            return Ok(types);
        }

        [HttpGet("UserPublications")]
        public async Task<ActionResult> GetUserPublications()
        {
            var userId = int.Parse(_currentUserService.UserId);

            var publications = await _publicationsService.GetUserPublications(userId);

            return Ok(publications);
        }

        [HttpGet("DepartmentPublications")]
        public async Task<ActionResult> GetDepartmentPublications()
        {
            var userId = int.Parse(_currentUserService.UserId);

            var publications = await _publicationsService.GetDepartmentPublications(userId);

            return Ok(publications);
        }

        [HttpPost("Publications")]
        public async Task<ActionResult> CreatePublication([FromBody] CreatePublicationDto dto)
        {
            var response = await _publicationsService.CreatePublication(dto);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(new { message = response.ErrorMessage });
            }

            return Ok(response.Value);
        }

        [HttpPut("Publications/{id}")]
        public async Task<ActionResult> UpdatePublication(int id, [FromBody] CreatePublicationDto dto)
        {
            var response = await _publicationsService.UpdatePublication(id, dto);
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                return BadRequest(new { message = response.ErrorMessage });
            }

            return Ok(response.Value);
        }

        [HttpDelete("Publications/{id}")]
        public async Task<ActionResult> DeletePublication(int id)
        {
            await _publicationsService.DeletePublication(id);

            return Ok();
        }

        [HttpGet("ScopusArticles")]
        public async Task<ActionResult> GetPublicationFromScopus([FromQuery] string articleNumber, [FromQuery] string title)
        {
            var publication = await _publicationsService.GetPublicationFromIeeeXplore(articleNumber, title);

            if (publication == null)
            {
                return BadRequest(new { message = "Статтю не знайдено." });
            }

            return Ok(publication);
        }

        [HttpPost("LoadScientificJournalsCategoryB")]
        public async Task<ActionResult> LoadScientificJournalsCategoryB()
        {
            await _publicationsService.LoadScientificJournalsCategoryB();

            return Ok();
        }

        [HttpPost("ImportScopusPublications")]
        public async Task<ActionResult> ImportScopusPublications()
        {
            await _publicationsService.ImportScopusPublications();

            return Ok();
        }
    }
}
