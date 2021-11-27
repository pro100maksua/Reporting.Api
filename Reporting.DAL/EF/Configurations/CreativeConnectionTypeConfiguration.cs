using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class CreativeConnectionTypeConfiguration : ComboboxItemConfiguration<CreativeConnectionType>
    {
        public override void Configure(EntityTypeBuilder<CreativeConnectionType> builder)
        {
            base.Configure(builder);

            builder.ToTable("CreativeConnectionTypes");
        }
    }
}
