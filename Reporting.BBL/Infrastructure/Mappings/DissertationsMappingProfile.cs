using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Reporting.Common.Constants;
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
                .ForMember(e => e.AuthorName, opt => opt.MapFrom(e => e.Author != null ? e.Author.Name : e.AuthorName));

            CreateMap<Dissertation, ReportDissertationDto>()
                .ForMember(e => e.Number,
                    opt => opt.MapFrom((e, _, _, c) =>
                        ((IList<Dissertation>)c.Items[DissertationsConstants.Dissertations]).IndexOf(e) + 1))
                .ForMember(e => e.DefenseDate, opt => opt.MapFrom(e => e.DefenseDate.ToString("dd.MM.yyyy р.")))
                .ForMember(e => e.DiplomaReceiptDate,
                    opt => opt.MapFrom(e =>
                        e.DiplomaReceiptDate != null ? e.DiplomaReceiptDate.Value.ToString("dd.MM.yyyy р.") : null))
                .ForMember(e => e.AuthorName, opt => opt.MapFrom(e => e.Author != null ? e.Author.Name : e.AuthorName));
        }
    }
}
