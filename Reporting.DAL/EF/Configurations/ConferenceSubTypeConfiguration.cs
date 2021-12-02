using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class ConferenceSubTypeConfiguration : ComboboxItemConfiguration<ConferenceSubType>
    {
        public override void Configure(EntityTypeBuilder<ConferenceSubType> builder)
        {
            base.Configure(builder);

            builder.ToTable("ConferenceSubTypes");
        }
    }
}
