using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class ActivityIndicatorConfiguration : AuditableEntityConfiguration<ActivityIndicator>
    {
        public override void Configure(EntityTypeBuilder<ActivityIndicator> builder)
        {
            base.Configure(builder);

            builder.ToTable("ActivityIndicator");

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
