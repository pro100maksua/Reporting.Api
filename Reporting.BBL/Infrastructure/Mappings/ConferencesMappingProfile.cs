using System.Globalization;
using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class ConferencesMappingProfile : MappingProfile
    {
        public ConferencesMappingProfile()
        {
            CreateMap<ConferenceType, ComboboxItemDto>();
            CreateMap<ConferenceSubType, ComboboxItemDto>();

            CreateMap<CreateConferenceDto, Conference>();

            CreateMap<Conference, ConferenceDto>()
                .ForMember(e => e.DateRange, opt => opt.MapFrom(e => GetDateRange(e)));
        }

        private static string GetDateRange(Conference conference)
        {
            if (conference.StartDate == null && conference.EndDate == null)
            {
                return null;
            }

            var culture = new CultureInfo("uk-UA");

            if (conference.StartDate == conference.EndDate)
            {
                return conference.StartDate?.ToString("dd MMMM yyyy", culture);
            }

            return
                $"{conference.StartDate?.Day}-{conference.EndDate?.Day} {conference.StartDate?.ToString("MMMM yyyy", culture)}";
        }
    }
}
