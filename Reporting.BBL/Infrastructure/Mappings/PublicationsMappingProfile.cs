using System;
using AutoMapper;
using Reporting.Common.ApiModels;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Infrastructure.Mappings
{
    public class PublicationsMappingProfile : Profile
    {
        public PublicationsMappingProfile()
        {
            CreateMap<PublicationType, ComboboxItemDto>();

            CreateMap<CreatePublicationDto, Publication>()
                .ForMember(p => p.Authors, opt => opt.MapFrom(a => string.Join(",", a.Authors)));

            CreateMap<Publication, PublicationDto>()
                .ForMember(p => p.Authors,
                    opt => opt.MapFrom(a => a.Authors.Split(',', StringSplitOptions.RemoveEmptyEntries)));

            CreateMap<string, PublicationAuthorDto>()
                .ForMember(p => p.FullName, opt => opt.MapFrom(s => s));

            CreateMap<ScopusArticle, PublicationDto>()
                .ForMember(p => p.Authors, opt => opt.MapFrom(a => a.Authors.Authors))
                .ForMember(p => p.PagesCount,
                    opt => opt.MapFrom(a => int.Parse(a.EndPage) - int.Parse(a.StartPage) + 1));

            CreateMap<ScopusAuthor, PublicationAuthorDto>();
        }
    }
}
