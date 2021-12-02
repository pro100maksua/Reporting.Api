using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class ConferenceTypeConfiguration : ComboboxItemConfiguration<ConferenceType>
    {
        public override void Configure(EntityTypeBuilder<ConferenceType> builder)
        {
            base.Configure(builder);

            builder.ToTable("ConferenceTypes");
        }
    }
}
