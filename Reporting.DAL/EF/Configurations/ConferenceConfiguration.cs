using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class ConferenceConfiguration : AuditableEntityConfiguration<Conference>
    {
        public override void Configure(EntityTypeBuilder<Conference> builder)
        {
            base.Configure(builder);

            builder.ToTable("Conferences");

            builder.Property(e => e.Title).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Location).HasMaxLength(500);

            builder.Property(e => e.Number).HasMaxLength(500);
        }
    }
}
