using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Reporting.BBL.Infrastructure;
using Reporting.BBL.Interfaces;
using Reporting.BBL.Models;
using Reporting.Common.Constants;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IPublicationsRepository _publicationsRepository;
        private readonly IRepository<StudentsWorkType> _studentsWorkTypesRepository;
        private readonly IRepository<StudentsScientificWorkType> _studentsScientificWorkTypesRepository;
        private readonly IStudentsWorkRepository _studentsWorkRepository;
        private readonly IRepository<PublicationType> _publicationTypeRepository;
        private readonly IConferencesRepository _conferencesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly WordHelper _wordHelper;
        private readonly IConfiguration _configuration;

        public ReportsService(IPublicationsRepository publicationsRepository,
            IRepository<StudentsWorkType> studentsWorkTypesRepository,
            IRepository<StudentsScientificWorkType> studentsScientificWorkTypesRepository,
            IStudentsWorkRepository studentsWorkRepository,
            IRepository<PublicationType> publicationTypeRepository,
            IConferencesRepository conferencesRepository,
            IRepository<User> usersRepository,
            WordHelper wordHelper,
            IConfiguration configuration)
        {
            _publicationsRepository = publicationsRepository;
            _studentsWorkTypesRepository = studentsWorkTypesRepository;
            _studentsScientificWorkTypesRepository = studentsScientificWorkTypesRepository;
            _studentsWorkRepository = studentsWorkRepository;
            _publicationTypeRepository = publicationTypeRepository;
            _conferencesRepository = conferencesRepository;
            _usersRepository = usersRepository;
            _wordHelper = wordHelper;
            _configuration = configuration;
        }

        public async Task<FileDto> DownloadDepartmentReports(int userId, int[] reportValues)
        {
            var reports = new List<byte[]>();

            var actions = new Dictionary<int, Func<Department, Task<byte[]>>>
            {
                { ReportsConstants.Report1, GenerateDepartmentReport1 },
                { ReportsConstants.Report3, GenerateDepartmentReport3 },
            };

            var user = await _usersRepository.Get(e => e.Id == userId, new[] { nameof(User.Department) });

            foreach (var value in reportValues)
            {
                if (actions.TryGetValue(value, out var action))
                {
                    reports.Add(await action(user.Department));
                }
            }

            var file = reports.Count > 1 ? _wordHelper.MergeDocuments(reports) : reports.First();

            return new FileDto
            {
                Bytes = file,
                ContentType = ReportsConstants.DocxContentType,
            };
        }

        public async Task<FileDto> GetUserReport3File(int userId)
        {
            var user = await _usersRepository.Get(e => e.Id == userId, new[] { nameof(User.Department) });
            var publications = await _publicationsRepository.GetUserPublications(userId, DateTime.Today.Year);
            var publicationTypes = await _publicationTypeRepository.GetAll();

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templateFilePath = Path.Combine(directory, _configuration[ReportsConstants.Report3FilePath]);

            var pdf = _wordHelper.GenerateReport3(user.Department, publications, publicationTypes, templateFilePath);

            return pdf == null
                ? null
                : new FileDto
                {
                    Bytes = pdf,
                    ContentType = ReportsConstants.DocxContentType,
                };
        }

        private async Task<byte[]> GenerateDepartmentReport1(Department department)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templateFilePath = Path.Combine(directory, _configuration[ReportsConstants.Report1FilePath]);

            var data = new Report1Data(department,
                await _publicationsRepository.GetDepartmentPublications(department.Id, DateTime.Today.Year),
                await _publicationTypeRepository.GetAll(),
                await _conferencesRepository.GetDepartmentConferences(department.Id, DateTime.Today.Year),
                await _studentsWorkRepository.GetStudentsWorkEntries(department.Id, DateTime.Today.Year),
                await _studentsWorkTypesRepository.GetAll(),
                await _studentsScientificWorkTypesRepository.GetAll());

            var pdf = _wordHelper.GenerateReport1(data, templateFilePath);

            return pdf;
        }

        private async Task<byte[]> GenerateDepartmentReport3(Department department)
        {
            var publications =
                await _publicationsRepository.GetDepartmentPublications(department.Id, DateTime.Today.Year);
            var publicationTypes = await _publicationTypeRepository.GetAll();

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templateFilePath = Path.Combine(directory, _configuration[ReportsConstants.Report3FilePath]);

            var pdf = _wordHelper.GenerateReport3(department, publications, publicationTypes, templateFilePath);

            return pdf;
        }
    }
}
