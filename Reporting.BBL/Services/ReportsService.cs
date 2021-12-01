using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly ISimpleRepository _repository;
        private readonly IActivityIndicatorsRepository _activityIndicatorsRepository;
        private readonly IDissertationsRepository _dissertationsRepository;
        private readonly IPublicationsRepository _publicationsRepository;
        private readonly IStudentsWorkRepository _studentsWorkRepository;
        private readonly IConferencesRepository _conferencesRepository;
        private readonly ICreativeConnectionsRepository _creativeConnectionsRepository;
        private readonly WordHelper _wordHelper;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ReportsService(ISimpleRepository repository,
            IActivityIndicatorsRepository activityIndicatorsRepository,
            IDissertationsRepository dissertationsRepository,
            IPublicationsRepository publicationsRepository,
            IStudentsWorkRepository studentsWorkRepository,
            IConferencesRepository conferencesRepository,
            ICreativeConnectionsRepository creativeConnectionsRepository,
            WordHelper wordHelper,
            IConfiguration configuration,
            IMapper mapper)
        {
            _repository = repository;
            _activityIndicatorsRepository = activityIndicatorsRepository;
            _dissertationsRepository = dissertationsRepository;
            _publicationsRepository = publicationsRepository;
            _studentsWorkRepository = studentsWorkRepository;
            _conferencesRepository = conferencesRepository;
            _creativeConnectionsRepository = creativeConnectionsRepository;
            _wordHelper = wordHelper;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<FileDto> DownloadDepartmentReports(int userId, int[] reportValues)
        {
            var reports = new List<byte[]>();

            var actions = new Dictionary<int, Func<Department, Task<byte[]>>>
            {
                { ReportsConstants.Report1, GenerateDepartmentReport1 },
                { ReportsConstants.Report2, GenerateDepartmentReport2 },
                { ReportsConstants.Report3, GenerateDepartmentReport3 },
            };

            var user = await _repository.Get<User>(e => e.Id == userId, new[] { nameof(User.Department) });

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
            var user = await _repository.Get<User>(e => e.Id == userId, new[] { nameof(User.Department) });
            var publications = await _publicationsRepository.GetUserPublications(userId, DateTime.Today.Year);
            var publicationTypes = await _repository.GetAll<PublicationType>();

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
                await _activityIndicatorsRepository.Get(e => e.DepartmentId == department.Id && e.Year == DateTime.Today.Year),
                await _publicationsRepository.GetDepartmentPublications(department.Id, DateTime.Today.Year),
                await _repository.GetAll<PublicationType>(),
                await _conferencesRepository.GetDepartmentConferences(department.Id, DateTime.Today.Year),
                await _creativeConnectionsRepository.GetDepartmentCreativeConnections(department.Id),
                await _repository.GetAll<CreativeConnectionType>(),
                await _studentsWorkRepository.GetStudentsWorkEntries(department.Id, DateTime.Today.Year),
                await _repository.GetAll<StudentsWorkType>(),
                await _repository.GetAll<StudentsScientificWorkType>());

            var pdf = _wordHelper.GenerateReport1(data, templateFilePath);

            return pdf;
        }

        private async Task<byte[]> GenerateDepartmentReport2(Department department)
        {
            var dissertations =
                await _dissertationsRepository.GetDepartmentDissertations(department.Id, DateTime.Today.Year);

            var dtos = _mapper.Map<IEnumerable<ReportDissertationDto>>(dissertations,
                opt => opt.Items[DissertationsConstants.Dissertations] = dissertations);

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templateFilePath = Path.Combine(directory, _configuration[ReportsConstants.Report2FilePath]);

            var pdf = _wordHelper.GenerateReport2(department, dtos, templateFilePath);

            return pdf;
        }

        private async Task<byte[]> GenerateDepartmentReport3(Department department)
        {
            var publications =
                await _publicationsRepository.GetDepartmentPublications(department.Id, DateTime.Today.Year);
            var publicationTypes = await _repository.GetAll<PublicationType>();

            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var templateFilePath = Path.Combine(directory, _configuration[ReportsConstants.Report3FilePath]);

            var pdf = _wordHelper.GenerateReport3(department, publications, publicationTypes, templateFilePath);

            return pdf;
        }
    }
}
