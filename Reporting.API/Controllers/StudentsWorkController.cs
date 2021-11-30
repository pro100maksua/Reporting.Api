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
    public class StudentsWorkController : ControllerBase
    {
        private readonly IStudentsWorkService _studentsWorkService;

        public StudentsWorkController(IStudentsWorkService studentsWorkService)
        {
            _studentsWorkService = studentsWorkService;
        }

        [HttpGet("StudentsWorkTypes")]
        public async Task<ActionResult> GetStudentsWorkTypes()
        {
            var types = await _studentsWorkService.GetStudentsWorkTypes();

            return Ok(types);
        }

        [HttpGet("StudentsScientificWorkTypes")]
        public async Task<ActionResult> GetStudentsScientificWorkTypes()
        {
            var types = await _studentsWorkService.GetStudentsScientificWorkTypes();

            return Ok(types);
        }

        [HttpGet("StudentsWorkEntries")]
        public async Task<ActionResult> GetStudentsWorkEntries()
        {
            var studentsWorkEntries = await _studentsWorkService.GetStudentsWorkEntries();

            return Ok(studentsWorkEntries);
        }

        [HttpPost("StudentsWorkEntries")]
        public async Task<ActionResult> CreateStudentsWorkEntry([FromBody] CreateStudentsWorkEntryDto dto)
        {
            await _studentsWorkService.CreateStudentsWorkEntry(dto);

            return Ok();
        }

        [HttpPut("StudentsWorkEntries/{id}")]
        public async Task<ActionResult> UpdateStudentsWorkEntry(int id, [FromBody] CreateStudentsWorkEntryDto dto)
        {
            await _studentsWorkService.UpdateStudentsWorkEntry(id, dto);

            return Ok();
        }

        [HttpDelete("StudentsWorkEntries/{id}")]
        public async Task<ActionResult> DeleteStudentsWorkEntry(int id)
        {
            await _studentsWorkService.DeleteStudentsWorkEntry(id);

            return Ok();
        }
    }
}
