using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class ActivityIndicatorsMappingProfile : Profile
    {
        public ActivityIndicatorsMappingProfile()
        {
            CreateMap<CreateActivityIndicatorDto, ActivityIndicator>();

            CreateMap<ActivityIndicator, ActivityIndicatorDto>();
        }
    }
}
