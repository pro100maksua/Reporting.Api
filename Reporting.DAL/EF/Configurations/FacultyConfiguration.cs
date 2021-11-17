using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class FacultyConfiguration : ComboboxItemConfiguration<Faculty>
    {
        public override void Configure(EntityTypeBuilder<Faculty> builder)
        {
            base.Configure(builder);

            builder.ToTable("Faculties");
        }
    }
}
