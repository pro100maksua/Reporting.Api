using System.Linq;
using AutoMapper;
using Reporting.Common.Constants;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class StudentsWorkMappingProfile : Profile
    {
        public StudentsWorkMappingProfile()
        {
            CreateMap<StudentsScientificWorkType, ComboboxItemDto>();

            CreateMap<StudentsWorkType, ComboboxItemDto>();

            CreateMap<CreateStudentsWorkEntryDto, StudentsWorkEntry>();

            CreateMap<StudentsWorkEntry, StudentsWorkEntryDto>()
                .ForMember(e => e.EntryName, opt => opt.MapFrom(e => GetEntryName(e)))
                .ForMember(e => e.Year, opt => opt.MapFrom(e => e.Created.Year));
        }

        private static string GetEntryName(StudentsWorkEntry entry)
        {
            var name = string.Empty;

            switch (entry.Type.Value)
            {
                case AppConstants.ParticipationInScientificWork:
                {
                    name += entry.ScientificWorkType.Name;

                    if (!string.IsNullOrEmpty(entry.ScientificWorkName))
                    {
                        name += $" - {entry.ScientificWorkName}";
                    }

                    break;
                }

                case AppConstants.ParticipationInCompetitions:
                case AppConstants.ReceivedAwardsForTheResultsOfTheSecondRound:
                case AppConstants.ParticipationInCompetitionsOfScientificWorksReceivedAwards:
                case AppConstants.ParticipationInCompetitionsOfDiplomaAndMastersReceivedAwards:
                {
                    name += $"{entry.Name} - {entry.Group} - {entry.Specialty} - {entry.Place} місце";

                    break;
                }

                case AppConstants.PublishedArticleAbstracts:
                {
                    if (entry.Independently)
                    {
                        name += "Cамостійно";
                    }

                    break;
                }
            }

            return name;
        }
    }
}
