using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginDto dto);
        Task<string> Register(RegisterDto dto);
    }
}
