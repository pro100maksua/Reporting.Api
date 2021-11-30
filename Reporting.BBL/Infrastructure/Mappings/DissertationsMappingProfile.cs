using AutoMapper;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class DissertationsMappingProfile : MappingProfile
    {
        public DissertationsMappingProfile()
        {
            CreateMap<CreateDissertationDto, Dissertation>()
                .ForMember(e => e.AuthorName, opt => opt.MapFrom((d, e) => e.AuthorId == null ? d.AuthorName : null));

            CreateMap<Dissertation, DissertationDto>()
                .ForMember(e => e.AuthorName,
                    opt => opt.MapFrom(e =>
                        e.Author != null ? $"{e.Author.FirstName} {e.Author.LastName}" : e.AuthorName));
        }
    }
}
