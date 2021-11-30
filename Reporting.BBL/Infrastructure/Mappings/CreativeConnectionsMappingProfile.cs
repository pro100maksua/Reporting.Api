using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class CreativeConnectionsMappingProfile : Profile
    {
        public CreativeConnectionsMappingProfile()
        {
            CreateMap<CreativeConnectionType, ComboboxItemDto>();

            CreateMap<CreateCreativeConnectionDto, CreativeConnection>();

            CreateMap<CreativeConnection, CreativeConnectionDto>();
        }
    }
}
