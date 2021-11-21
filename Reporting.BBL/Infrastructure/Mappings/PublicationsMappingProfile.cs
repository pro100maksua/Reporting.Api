using System;
using System.Linq;
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

            CreateMap<CreatePublicationDto, Publication>();

            CreateMap<Publication, PublicationDto>()
                .ForMember(p => p.Authors,
                    opt => opt.MapFrom(a =>
                        string.IsNullOrEmpty(a.ScopusAuthors)
                            ? string.Join(", ", a.Authors.Select(u => $"{u.FirstName} {u.LastName}"))
                            : a.ScopusAuthors));

            CreateMap<string, PublicationAuthorDto>()
                .ForMember(p => p.FullName, opt => opt.MapFrom(s => s));

            CreateMap<IeeeXploreArticle, PublicationDto>()
                .ForMember(p => p.ScopusAuthors,
                    opt => opt.MapFrom(a => string.Join(", ", a.Authors.Authors.Select(u => u.FullName))))
                .ForMember(p => p.PagesCount,
                    opt => opt.MapFrom(a => int.Parse(a.EndPage) - int.Parse(a.StartPage) + 1));

            CreateMap<IeeeXploreAuthor, PublicationAuthorDto>();
        }
    }
}
