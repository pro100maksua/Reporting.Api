using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class ConferencesMappingProfile : Profile
    {
        public ConferencesMappingProfile()
        {
            CreateMap<CreateConferenceDto, Conference>();

            CreateMap<Conference, ConferenceDto>();
        }
    }
}
