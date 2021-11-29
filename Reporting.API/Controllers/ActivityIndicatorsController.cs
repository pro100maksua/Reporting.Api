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
    public class ActivityIndicatorsController : ControllerBase
    {
        private readonly IActivityIndicatorsService _activityIndicatorsService;

        public ActivityIndicatorsController(IActivityIndicatorsService activityIndicatorsService)
        {
            _activityIndicatorsService = activityIndicatorsService;
        }

        [HttpGet("ActivityIndicators")]
        public async Task<ActionResult> GetActivityIndicators()
        {
            var activityIndicators = await _activityIndicatorsService.GetDepartmentActivityIndicators();

            return Ok(activityIndicators);
        }

        [HttpGet("ActivityIndicator")]
        public async Task<ActionResult> GetActivityIndicator([FromQuery] int year)
        {
            var activityIndicator = await _activityIndicatorsService.GetDepartmentActivityIndicator(year);

            return Ok(activityIndicator);
        }

        [HttpPost("ActivityIndicators")]
        public async Task<ActionResult> CreateActivityIndicator([FromBody] CreateActivityIndicatorDto dto)
        {
            await _activityIndicatorsService.CreateActivityIndicator(dto);

            return Ok();
        }

        [HttpPut("ActivityIndicators/{id}")]
        public async Task<ActionResult> UpdateActivityIndicator(int id, [FromBody] CreateActivityIndicatorDto dto)
        {
            await _activityIndicatorsService.UpdateActivityIndicator(id, dto);

            return Ok();
        }

        [HttpDelete("ActivityIndicators/{id}")]
        public async Task<ActionResult> DeleteActivityIndicator(int id)
        {
            await _activityIndicatorsService.DeleteActivityIndicator(id);

            return Ok();
        }
    }
}
