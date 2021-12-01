using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Reporting.Common.Constants;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class UsersMappingProfile : MappingProfile
    {
        public UsersMappingProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(e => e.Password, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ForMember(e => e.Roles, opt => opt.MapFrom(e => e.Roles.Select(r => r.Value)))
                .ForMember(e => e.RoleId, opt => opt.MapFrom(e => e.Roles.FirstOrDefault().Id));

            CreateMap<User, Report5UserDto>()
                .ForMember(e => e.Number,
                    opt => opt.MapFrom((e, _, _, c) =>
                        ((List<User>)c.Items[ReportsConstants.Users]).IndexOf(e) + 1))
                .ForMember(e => e.PublicationsPrintedPagesCount,
                    opt => opt.MapFrom(e => e.Publications.Sum(p => p.PrintedPagesCount)));

            CreateMap<Role, ComboboxItemDto>();

            CreateMap<Faculty, ComboboxItemDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
