using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto dto);
        Task<ResponseDto<string>> Register(RegisterDto dto);
        Task<string> ValidateEmail(ValidateValueDto dto);
    }
}
