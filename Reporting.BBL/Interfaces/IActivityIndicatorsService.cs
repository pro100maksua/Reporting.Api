using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IActivityIndicatorsService
    {
        Task<IEnumerable<ActivityIndicatorDto>> GetDepartmentActivityIndicators();
        Task<ActivityIndicatorDto> GetDepartmentActivityIndicator(int year);
        Task CreateActivityIndicator(CreateActivityIndicatorDto dto);
        Task UpdateActivityIndicator(int id, CreateActivityIndicatorDto dto);
        Task DeleteActivityIndicator(int id);
    }
}
