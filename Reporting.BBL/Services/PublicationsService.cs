using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly ICurrentUserService _currentUserService;
        private readonly IHtmlParserService _htmlParserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublicationsRepository _publicationsRepository;
        private readonly IRepository<Conference> _conferencesRepository;
        private readonly IRepository<PublicationType> _publicationTypeRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public PublicationsService(IIeeeXploreApi ieeeXploreApi,
            ICurrentUserService currentUserService,
            IHtmlParserService htmlParserService,
            IUnitOfWork unitOfWork,
            IPublicationsRepository publicationsRepository,
            IRepository<Conference> conferencesRepository,
            IRepository<PublicationType> publicationTypeRepository,
            IRepository<User> usersRepository,
            IConfiguration configuration,
            IMapper mapper,
            IMemoryCache cache)
        {
            _ieeeXploreApi = ieeeXploreApi;
            _currentUserService = currentUserService;
            _htmlParserService = htmlParserService;
            _unitOfWork = unitOfWork;
            _publicationsRepository = publicationsRepository;
            _conferencesRepository = conferencesRepository;
            _publicationTypeRepository = publicationTypeRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
            _cache = cache;

            _ieeeXploreApi.ApiKey = configuration[AppConstants.IeeeXploreApiKey];
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetPublicationTypes()
        {
            var types = await _publicationTypeRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<PublicationDto>> GetUserPublications(int userId)
        {
            var publications = await _publicationsRepository.GetUserPublications(userId);

            var dtos = _mapper.Map<IEnumerable<PublicationDto>>(publications);

            return dtos;
        }

        public async Task<IEnumerable<PublicationDto>> GetDepartmentPublications(int userId)
        {
            var user = await _usersRepository.Get(userId);

            var publications = await _publicationsRepository.GetDepartmentPublications(user.DepartmentId);

            var dtos = _mapper.Map<IEnumerable<PublicationDto>>(publications);

            return dtos;
        }

        public async Task<ResponseDto<PublicationDto>> CreatePublication(CreatePublicationDto dto)
        {
            var response = new ResponseDto<PublicationDto>();

            var errorMessage = await ValidationPublication(dto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                return response;
            }

            var publication = await _publicationsRepository.Get(e =>
                (dto.ArticleNumber != null && e.ArticleNumber == dto.ArticleNumber) ||
                e.Title == dto.Title, new[] { nameof(Publication.Authors) });

            if (publication != null)
            {
                await UpdatePublication(publication, dto);
            }
            else
            {
                publication = _mapper.Map<CreatePublicationDto, Publication>(dto);

                await CreateOrSetConference(publication, dto);

                await _publicationsRepository.Add(publication);
            }

            var userId = int.Parse(_currentUserService.UserId);
            if (publication.Authors.All(e => e.Id != userId))
            {
                var author = await _usersRepository.Get(userId);
                publication.Authors.Add(author);
            }

            await _unitOfWork.SaveChanges();

            publication =
                await _publicationsRepository.Get(p => p.Id == publication.Id, new[] { nameof(Publication.Type) });

            response.Value = _mapper.Map<Publication, PublicationDto>(publication);
            return response;
        }

        public async Task<ResponseDto<PublicationDto>> UpdatePublication(int id, CreatePublicationDto dto)
        {
            var response = new ResponseDto<PublicationDto>();

            var errorMessage = await ValidationPublication(dto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                return response;
            }

            var publication = await _publicationsRepository.Get(id);

            if (publication == null)
            {
                return null;
            }

            await UpdatePublication(publication, dto);

            await _unitOfWork.SaveChanges();

            publication =
                await _publicationsRepository.Get(p => p.Id == publication.Id, new[] { nameof(Publication.Type) });

            response.Value = _mapper.Map<Publication, PublicationDto>(publication);
            return response;
        }

        public async Task DeletePublication(int id)
        {
            await _publicationsRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }

        public async Task<PublicationDto> GetPublicationFromIeeeXplore(string articleNumber, string title)
        {
            articleNumber = articleNumber?.Trim();
            title = title?.Trim();

            if (string.IsNullOrEmpty(articleNumber) && string.IsNullOrEmpty(title))
            {
                return null;
            }

            var result = await _ieeeXploreApi.GetArticlesAsync(articleNumber, title);

            var article = result.Articles?.FirstOrDefault(a => a.ArticleNumber == articleNumber || a.Title == title);

            var dto = _mapper.Map<IeeeXploreArticle, PublicationDto>(article);

            return dto;
        }

        public async Task LoadScientificJournalsCategoryB()
        {
            //if (!_cache.TryGetValue(AppConstants.ScientificJournalsCategoryB, out _))
            //{
            //    var journals = await _htmlParserService.GetScientificJournalsCategoryB();

            //    _cache.Set(AppConstants.ScientificJournalsCategoryB, journals, TimeSpan.FromDays(1));
            //}
        }

        public async Task ImportScopusPublications()
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _usersRepository.Get(userId);

            if (string.IsNullOrEmpty(user.IeeeXploreAuthorName))
            {
                return;
            }

            var result = await _ieeeXploreApi.GetAuthorArticlesAsync(user.IeeeXploreAuthorName);

            var scopusType = await _publicationTypeRepository.Get(e => e.Value == PublicationsConstants.ScopusPublicationType);

            var publications =
                _mapper.Map<IEnumerable<IeeeXploreArticle>, IEnumerable<CreatePublicationDto>>(result.Articles,
                    opt => opt.Items[PublicationsConstants.ScopusTypeId] = scopusType.Id);

            foreach (var publication in publications)
            {
                await CreatePublication(publication);
            }
        }

        private async Task<string> ValidationPublication(CreatePublicationDto dto)
        {
            var type = await _publicationTypeRepository.Get(dto.TypeId);

            if (type.Value == PublicationsConstants.CategoryBPublicationType)
            {
                //var journals = await GetScientificJournalsCategoryB();

                //if (!journals.Contains(dto.PublicationTitle))
                //{
                //    return "Дане видавництво категорії Б не знайдено";
                //}
            }

            return null;
        }

        private async Task<IEnumerable<string>> GetScientificJournalsCategoryB()
        {
            if (!_cache.TryGetValue(PublicationsConstants.ScientificJournalsCategoryB, out IEnumerable<string> cachedJournals))
            {
                cachedJournals = await _htmlParserService.GetScientificJournalsCategoryB();

                _cache.Set(PublicationsConstants.ScientificJournalsCategoryB, cachedJournals, TimeSpan.FromDays(1));
            }

            return cachedJournals;
        }

        private async Task UpdatePublication(Publication publication, CreatePublicationDto dto)
        {
            _mapper.Map(dto, publication);
        }

        private async Task CreateOrSetConference(Publication publication, CreatePublicationDto dto)
        {
            if (dto.ContentType == PublicationsConstants.Conferences)
            {
                var conference = await _conferencesRepository.Get(e => e.Number == dto.PublicationNumber);
                if (conference != null)
                {
                    publication.ConferenceId = conference.Id;
                }
                else
                {
                    publication.Conference = new Conference
                    {
                        Number = dto.PublicationNumber,
                        Location = dto.ConferenceLocation,
                        Title = dto.PublicationTitle,
                        Year = dto.PublicationYear,
                    };
                }
            }
        }
    }
}
