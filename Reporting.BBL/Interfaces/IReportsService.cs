using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IReportsService
    {
        Task<FileDto> DownloadDepartmentReports(int userId, IEnumerable<int> reportValues, int year);
        Task<FileDto> GetUserReport3File(int userId);
    }
}
