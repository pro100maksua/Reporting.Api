using System.Collections.Generic;
using System.Globalization;
using AutoMapper;
using Reporting.Common.Constants;
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

            CreateMap<Conference, ReportConferenceDto>()
                .ForMember(e => e.Number, opt => opt.MapFrom((e, _, _, c) => GetNumber(e, c)))
                .ForMember(e => e.DateRange, opt => opt.MapFrom(e => GetDateRange(e)));
        }

        private static string GetNumber(Conference conference, ResolutionContext context)
        {
            var index = ((List<Conference>)context.Items[ReportsConstants.Conferences]).IndexOf(conference);

            return $"{context.Items[ReportsConstants.NumberPrefix]}{index + 1}";
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
