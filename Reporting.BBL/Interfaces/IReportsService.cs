using System.Collections.Generic;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Interfaces
{
    public interface IReportsService
    {
        byte[] GenerateCalibrationDispositionPdf(User user, IEnumerable<Publication> publications,
            IEnumerable<PublicationType> publicationTypes, string templateFilePath);
    }
}
