using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class PublicationConfiguration : AuditableEntityConfiguration<Publication>
    {
        public override void Configure(EntityTypeBuilder<Publication> builder)
        {
            base.Configure(builder);

            builder.ToTable("Publications");

            builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
            builder.Property(e => e.PublicationTitle).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Authors).IsRequired().HasMaxLength(500);
        }
    }
}
