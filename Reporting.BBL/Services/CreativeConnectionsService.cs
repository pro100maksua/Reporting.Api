using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class CreativeConnectionsService : ICreativeConnectionsService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreativeConnectionsRepository _creativeConnectionsRepository;
        private readonly ISimpleRepository _repository;
        private readonly IMapper _mapper;

        public CreativeConnectionsService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ICreativeConnectionsRepository creativeConnectionsRepository,
            ISimpleRepository repository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _creativeConnectionsRepository = creativeConnectionsRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetCreativeConnectionTypes()
        {
            var types = await _repository.GetAll<CreativeConnectionType>();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<CreativeConnectionDto>> GetDepartmentCreativeConnections()
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var entries = await _creativeConnectionsRepository.GetDepartmentCreativeConnections(user.DepartmentId);

            var dtos = _mapper.Map<IEnumerable<CreativeConnectionDto>>(entries);

            return dtos;
        }

        public async Task CreateCreativeConnection(CreateCreativeConnectionDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var entry = _mapper.Map<CreateCreativeConnectionDto, CreativeConnection>(dto);
            entry.DepartmentId = user.DepartmentId;

            await _creativeConnectionsRepository.Add(entry);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateCreativeConnection(int id, CreateCreativeConnectionDto dto)
        {
            var entry = await _creativeConnectionsRepository.Get(id);

            if (entry == null)
            {
                return;
            }

            _mapper.Map(dto, entry);

            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteCreativeConnection(int id)
        {
            await _creativeConnectionsRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
