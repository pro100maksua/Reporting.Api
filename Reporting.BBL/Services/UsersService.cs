using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<Faculty> _facultiesRepository;
        private readonly IRepository<Department> _departmentsRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWork unitOfWork, IRepository<User> usersRepository,
            IRepository<Role> rolesRepository, IRepository<Faculty> facultiesRepository,
            IRepository<Department> departmentsRepository, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _facultiesRepository = facultiesRepository;
            _departmentsRepository = departmentsRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetRoles()
        {
            var roles = await _rolesRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(roles);

            return dtos;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetFaculties()
        {
            var faculties = await _facultiesRepository.GetAll();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(faculties);

            return dtos;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartments(int facultyValue)
        {
            var departments = await _departmentsRepository.GetAll(e => e.Faculty.Value == facultyValue);

            var dtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return dtos;
        }

        public async Task UpdateUserIeeeXploreAuthorName(int userId, string name)
        {
            var user = await _usersRepository.Get(userId);
            if (user == null)
            {
                return;
            }

            user.IeeeXploreAuthorName = name;

            await _unitOfWork.SaveChanges();
        }
    }
}
