using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Reporting.BBL.ApiInterfaces;
using Reporting.BBL.Interfaces;
using Reporting.Common.ApiModels;
using Reporting.Common.Constants;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class PublicationsService : IPublicationsService
    {
        private readonly IIeeeXploreApi _ieeeXploreApi;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Publication> _publicationRepository;
        private readonly IRepository<PublicationType> _publicationTypeRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PublicationsService(IIeeeXploreApi ieeeXploreApi, IUnitOfWork unitOfWork,
            IRepository<Publication> publicationRepository, IRepository<PublicationType> publicationTypeRepository,
            IConfiguration configuration, IMapper mapper)
        {
            _ieeeXploreApi = ieeeXploreApi;
            _unitOfWork = unitOfWork;
            _publicationRepository = publicationRepository;
            _publicationTypeRepository = publicationTypeRepository;
            _configuration = configuration;
            _mapper = mapper;

            _ieeeXploreApi.ApiKey = configuration[AppConstants.IeeeXploreApiKey];
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetPublicationTypes()
        {
            var types = await _publicationTypeRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<PublicationDto>> GetPublications()
        {
            var types = await _publicationRepository.GetAll(
                includeProperties: new[] { nameof(Publication.Type) });

            var dtos = _mapper.Map<IEnumerable<PublicationDto>>(types);

            return dtos;
        }

        public async Task<PublicationDto> CreatePublication(CreatePublicationDto dto)
        {
            var publication = _mapper.Map<CreatePublicationDto, Publication>(dto);

            await _publicationRepository.Add(publication);
            await _unitOfWork.SaveChanges();

            publication =
                await _publicationRepository.Get(p => p.Id == publication.Id, new[] { nameof(Publication.Type) });

            return _mapper.Map<Publication, PublicationDto>(publication);
        }

        public async Task DeletePublication(int id)
        {
            await _publicationRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }

        public async Task<PublicationDto> GetPublicationFromScopus(string articleNumber, string title)
        {
            articleNumber = articleNumber?.Trim();
            title = title?.Trim();

            if (string.IsNullOrEmpty(articleNumber) && string.IsNullOrEmpty(title))
            {
                return null;
            }

            var result = await _ieeeXploreApi.GetArticlesAsync(articleNumber, title);

            var article = result.Articles?.FirstOrDefault(a => a.ArticleNumber == articleNumber || a.Title == title);

            var dto = _mapper.Map<ScopusArticle, PublicationDto>(article);

            return dto;
        }
    }
}
