using System.Linq;
using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(e => e.Password, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(e => e.Roles, opt => opt.MapFrom(e => e.Roles.Select(r => r.Value)))
                .ForMember(e => e.RoleId, opt => opt.MapFrom(e => e.Roles.FirstOrDefault().Id));

            CreateMap<Role, ComboboxItemDto>();

            CreateMap<Faculty, ComboboxItemDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
