using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reporting.BBL.Interfaces;

namespace Reporting.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly ICurrentUserService _currentUserService;

        public ReportsController(IReportsService reportsService, ICurrentUserService currentUserService)
        {
            _reportsService = reportsService;
            _currentUserService = currentUserService;
        }

        [HttpGet("DownloadDepartmentReports")]
        public async Task<ActionResult> DownloadDepartmentReports([FromQuery] int[] reports)
        {
            var userId = int.Parse(_currentUserService.UserId);

            var file = await _reportsService.DownloadDepartmentReports(userId, reports);

            if (file == null)
            {
                return NotFound(new { message = "Файл не знайдено." });
            }

            return File(file.Bytes, file.ContentType, file.FileName);
        }

        [HttpGet("UserReport3File")]
        public async Task<ActionResult> GetUserReport3File()
        {
            var userId = int.Parse(_currentUserService.UserId);

            var file = await _reportsService.GetUserReport3File(userId);

            if (file == null)
            {
                return NotFound(new { message = "Файл не знайдено." });
            }

            return File(file.Bytes, file.ContentType, file.FileName);
        }
    }
}
