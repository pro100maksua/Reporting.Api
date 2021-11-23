using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Reporting.BBL.Interfaces;
using Reporting.Domain.Entities;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;

namespace Reporting.BBL.Services
{
    public class ReportsService : IReportsService
    {
        public byte[] GenerateCalibrationDispositionPdf(User user, IEnumerable<Publication> publications,
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
                    user.Department.Name[(user.Department.Name.Split()[0].Length + 1)..],
                };

                document.MailMerge.Execute(fields, values);
            });
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
    }
}
