using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class CreativeConnectionConfiguration : AuditableEntityConfiguration<CreativeConnection>
    {
        public override void Configure(EntityTypeBuilder<CreativeConnection> builder)
        {
            base.Configure(builder);

            builder.ToTable("CreativeConnections");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Address).HasMaxLength(500);

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
