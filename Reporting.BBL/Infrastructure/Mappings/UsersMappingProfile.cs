using System;
using AutoMapper;
using Reporting.Common.ApiModels;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<RegisterDto, User>();

            CreateMap<Role, ComboboxItemDto>();

            CreateMap<Faculty, ComboboxItemDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
