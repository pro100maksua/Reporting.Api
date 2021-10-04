using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class PublicationTypeConfiguration : ComboboxItemConfiguration<PublicationType>
    {
        public override void Configure(EntityTypeBuilder<PublicationType> builder)
        {
            base.Configure(builder);

            builder.ToTable("PublicationTypes");
        }
    }
}
