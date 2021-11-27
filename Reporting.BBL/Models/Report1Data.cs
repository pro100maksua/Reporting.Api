using System.Collections.Generic;
using Reporting.Domain.Entities;

namespace Reporting.BBL.Models
{
    public record Report1Data(Department Department,
        IEnumerable<Publication> Publications,
        IEnumerable<PublicationType> PublicationTypes,
        IEnumerable<Conference> Conferences,
        IEnumerable<StudentsWorkEntry> StudentsWorkEntries,
        IEnumerable<StudentsWorkType> StudentsWorkTypes,
        IEnumerable<StudentsScientificWorkType> ScientificWorkTypes);
}
