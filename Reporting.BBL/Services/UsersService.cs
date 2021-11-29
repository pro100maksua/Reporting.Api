using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimpleRepository _repository;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWork unitOfWork, ISimpleRepository repository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetRoles()
        {
            var roles = await _repository.GetAll<Role>();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(roles);

            return dtos;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetFaculties()
        {
            var faculties = await _repository.GetAll<Faculty>();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(faculties);

            return dtos;
        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartments(int facultyValue)
        {
            var departments = await _repository.GetAll<Department>(e => e.Faculty.Value == facultyValue);

            var dtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return dtos;
        }

        public async Task UpdateUserIeeeXploreAuthorName(int userId, string name)
        {
            var user = await _repository.Get<User>(userId);
            if (user == null)
            {
                return;
            }

            user.IeeeXploreAuthorName = name;

            await _unitOfWork.SaveChanges();
        }
    }
}
