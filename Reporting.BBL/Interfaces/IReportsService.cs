using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IReportsService
    {
        Task<FileDto> DownloadDepartmentReports(int userId, int[] reportValues);
        Task<FileDto> GetUserReport3File(int userId);
    }
}
