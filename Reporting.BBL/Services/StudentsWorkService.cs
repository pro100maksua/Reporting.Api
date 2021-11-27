using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class StudentsWorkService : IStudentsWorkService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentsWorkRepository _studentsWorkRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<StudentsWorkType> _studentsWorkTypesRepository;
        private readonly IRepository<StudentsScientificWorkType> _studentsScientificWorkTypesRepository;
        private readonly IMapper _mapper;

        public StudentsWorkService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IStudentsWorkRepository studentsWorkRepository,
            IRepository<User> usersRepository,
            IRepository<StudentsWorkType> studentsWorkTypesRepository,
            IRepository<StudentsScientificWorkType> studentsScientificWorkTypesRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _studentsWorkRepository = studentsWorkRepository;
            _usersRepository = usersRepository;
            _studentsWorkTypesRepository = studentsWorkTypesRepository;
            _studentsScientificWorkTypesRepository = studentsScientificWorkTypesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetStudentsWorkTypes()
        {
            var types = await _studentsWorkTypesRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetStudentsScientificWorkTypes()
        {
            var types = await _studentsScientificWorkTypesRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<StudentsWorkEntryDto>> GetStudentsWorkEntries()
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _usersRepository.Get(userId);

            var entries = await _studentsWorkRepository.GetStudentsWorkEntries(user.DepartmentId);

            var dtos = _mapper.Map<IEnumerable<StudentsWorkEntryDto>>(entries);

            return dtos;
        }

        public async Task CreateStudentsWorkEntry(CreateStudentsWorkEntryDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);

            var entry = _mapper.Map<CreateStudentsWorkEntryDto, StudentsWorkEntry>(dto);
            entry.TeacherId = userId;

            await _studentsWorkRepository.Add(entry);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateStudentsWorkEntry(int id, CreateStudentsWorkEntryDto dto)
        {
            var entry = await _studentsWorkRepository.Get(id);

            if (entry == null)
            {
                return;
            }

            _mapper.Map(dto, entry);

            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteStudentsWorkEntry(int id)
        {
            await _studentsWorkRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
