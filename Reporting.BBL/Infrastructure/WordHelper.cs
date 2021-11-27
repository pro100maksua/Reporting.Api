using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Reporting.Common.Constants;
using Reporting.Domain.Entities;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace Reporting.BBL.Infrastructure
{
    public class WordHelper
    {
        public byte[] GenerateReport1(Department department,
            IEnumerable<Publication> publications,
            IEnumerable<PublicationType> publicationTypes,
            IEnumerable<StudentsWorkEntry> studentsWorkEntries,
            IEnumerable<StudentsWorkType> studentsWorkTypes,
            IEnumerable<StudentsScientificWorkType> scientificWorkTypes,
            string templateFilePath)
        {
            return GenerateDocument(templateFilePath,
                document =>
                {
                    var fields = new[] { "Year", "Department", };
                    var values = new[]
                    {
                        DateTime.Today.Year.ToString(),

                        // skip first word
                        department.Name[(department.Name.Split()[0].Length + 1)..],
                    };

                    document.MailMerge.ClearFields = false;
                    document.MailMerge.Execute(fields, values);

                    var (publicationsKeys, publicationsValues) = GetReport1PublicationsData(publications, publicationTypes);
                    document.MailMerge.Execute(publicationsKeys, publicationsValues);

                    var (studentsWorkKeys, studentsWorkValues) =
                        GetReport1StudentsWorkData(studentsWorkEntries, studentsWorkTypes, scientificWorkTypes);

                    document.MailMerge.ClearFields = true;
                    document.MailMerge.Execute(studentsWorkKeys, studentsWorkValues);
                });
        }

        public byte[] GenerateReport3(Department department, IEnumerable<Publication> publications,
           IEnumerable<PublicationType> publicationTypes, string templateFilePath)
        {
            return GenerateDocument(templateFilePath, document =>
            {
                foreach (var type in publicationTypes)
                {
                    var data = publications.Where(p => p.TypeId == type.Id).Select((p, i) => new
                    {
                        Number = i + 1,
                        Authors = string.IsNullOrEmpty(p.ScopusAuthors)
                            ? string.Join(", ", p.Authors.Select(u => $"{u.FirstName} {u.LastName}"))
                            : p.ScopusAuthors,
                        p.Title,
                        p.PublicationTitle,
                        Link = p.HtmlUrl,
                        p.PublicationYear,
                        Pages = $"{p.StartPage}-{p.EndPage}",
                        PagesCount = p.EndPage - p.StartPage + 1,
                        p.PrintedPagesCount,
                    }).ToList();

                    var dataTable = new MailMergeDataTable(type.Value.ToString(), data);

                    document.MailMerge.ExecuteGroup(dataTable);
                }

                var fields = new[] { "Year", "Department" };
                var values = new[]
                {
                    DateTime.Today.Year.ToString(),

                    // skip first word
                    department.Name[(department.Name.Split()[0].Length + 1)..],
                };

                document.MailMerge.Execute(fields, values);
            });
        }

        public byte[] MergeDocuments(IEnumerable<byte[]> reports)
        {
            using var destinationStream = new MemoryStream(reports.First());
            using var destinationDocument = new WordDocument(destinationStream, FormatType.Docx);

            foreach (var report in reports.Skip(1))
            {
                using var stream = new MemoryStream(report);
                using var document = new WordDocument(stream, FormatType.Automatic);

                destinationDocument.ImportContent(document, ImportOptions.UseDestinationStyles);

                document.Close();
            }

            using var documentStream = new MemoryStream();

            destinationDocument.Save(documentStream, FormatType.Docx);
            destinationDocument.Close();

            return documentStream.ToArray();
        }

        private byte[] GenerateDocument(string templateFilePath, Action<WordDocument> action)
        {
            using var fileStreamPath = new FileStream(templateFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var document = new WordDocument(fileStreamPath, FormatType.Automatic);

            action(document);

            using var documentStream = new MemoryStream();

            document.Save(documentStream, FormatType.Docx);
            document.Close();

            return documentStream.ToArray();
        }

        private (string[] keys, string[] values) GetReport1PublicationsData(
            IEnumerable<Publication> publications,
            IEnumerable<PublicationType> publicationTypes)
        {
            var keys = new List<string>();
            var values = new List<string>();

            keys.Add("Publications.Total");
            values.Add(GetPublicationsValue(publications));

            var journalTypes = new[]
            {
                PublicationsConstants.CategoryBPublicationType,
                PublicationsConstants.CategoryVPublicationType,
                PublicationsConstants.ScopusPublicationType,
                PublicationsConstants.WebOfSciencePublicationType,
                PublicationsConstants.ForeignPublicationType,
            };

            keys.Add("Publications.TotalInJournals");
            values.Add(GetPublicationsValue(publications.Where(e => journalTypes.Contains(e.Type.Value))));

            var ukrJournalTypes = new[]
            {
                PublicationsConstants.CategoryBPublicationType,
                PublicationsConstants.CategoryVPublicationType,
            };

            keys.Add("Publications.TotalInUkrJournals");
            values.Add(GetPublicationsValue(publications.Where(e => ukrJournalTypes.Contains(e.Type.Value))));

            var foreignJournalTypes = new[]
            {
                PublicationsConstants.ScopusPublicationType,
                PublicationsConstants.WebOfSciencePublicationType,
                PublicationsConstants.ForeignPublicationType,
            };

            keys.Add("Publications.TotalInForeignJournals");
            values.Add(GetPublicationsValue(publications.Where(e => foreignJournalTypes.Contains(e.Type.Value))));

            keys.AddRange(publicationTypes.Select(t => $"Publications.{t.Value}"));
            values.AddRange(publicationTypes.Select(t =>
                GetPublicationsValue(publications.Where(e => e.Type.Value == t.Value).ToList())));

            return (keys.ToArray(), values.ToArray());
        }

        private (string[] keys, string[] values) GetReport1StudentsWorkData(
            IEnumerable<StudentsWorkEntry> studentsWorkEntries,
            IEnumerable<StudentsWorkType> studentsWorkTypes,
            IEnumerable<StudentsScientificWorkType> scientificWorkTypes)
        {
            var keys = new List<string>();

            keys.AddRange(studentsWorkTypes.Select(t => $"Students{t.Value}"));
            keys.AddRange(scientificWorkTypes.Select(t =>
                $"Students{StudentsWorkConstants.Type1}.{t.Value}"));
            keys.Add($"Students{StudentsWorkConstants.Type7}.1");

            var values = new List<string>();

            values.AddRange(studentsWorkTypes.Select(t =>
                studentsWorkEntries.Count(e => e.Type.Value == t.Value).ToString()));

            values.AddRange(scientificWorkTypes.Select(t =>
            {
                var entries = studentsWorkEntries
                    .Where(e => e.Type.Value == StudentsWorkConstants.Type1)
                    .Where(e => e.ScientificWorkType.Value == t.Value)
                    .ToList();
                var groupCount = entries.GroupBy(e => e.ScientificWorkName).Count();

                return t.Value is StudentsWorkConstants.ContractResearch or
                    StudentsWorkConstants.DiplomaAndMasters
                    ? entries.Count.ToString()
                    : $"{groupCount}/{entries.Count}";
            }));

            values.Add(studentsWorkEntries
                .Count(e => e.Type.Value == StudentsWorkConstants.Type7 && e.Independently).ToString());

            return (keys.ToArray(), values.ToArray());
        }

        private string GetPublicationsValue(IEnumerable<Publication> publications)
        {
            return $"{publications.Count()}/{publications.Sum(p => p.PrintedPagesCount)}";
        }
    }
}
