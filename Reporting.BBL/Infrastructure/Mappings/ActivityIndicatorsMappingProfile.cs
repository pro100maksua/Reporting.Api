using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class ActivityIndicatorsMappingProfile : MappingProfile
    {
        public ActivityIndicatorsMappingProfile()
        {
            CreateMap<CreateActivityIndicatorDto, ActivityIndicator>();

            CreateMap<ActivityIndicator, ActivityIndicatorDto>();
        }
    }
}
