using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class StudentsWorkEntryTypeConfiguration : ComboboxItemConfiguration<StudentsWorkType>
    {
        public override void Configure(EntityTypeBuilder<StudentsWorkType> builder)
        {
            base.Configure(builder);

            builder.ToTable("StudentsWorkTypes");
        }
    }
}
