using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class StudentsScientificWorkTypeConfiguration : ComboboxItemConfiguration<StudentsScientificWorkType>
    {
        public override void Configure(EntityTypeBuilder<StudentsScientificWorkType> builder)
        {
            base.Configure(builder);

            builder.ToTable("StudentsScientificWorkTypes");
        }
    }
}
