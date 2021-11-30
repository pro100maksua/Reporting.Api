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
                case StudentsWorkConstants.Type1:
                {
                    name += entry.ScientificWorkType.Name;

                    if (!string.IsNullOrEmpty(entry.ScientificWorkName))
                    {
                        name += $" - {entry.ScientificWorkName}";
                    }

                    break;
                }

                case StudentsWorkConstants.Type2:
                case StudentsWorkConstants.Type3:
                case StudentsWorkConstants.Type5:
                case StudentsWorkConstants.Type6:
                {
                    name += $"{entry.Name} - {entry.Group} - {entry.Specialty} - {entry.Place} місце";

                    break;
                }

                case StudentsWorkConstants.Type7:
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
