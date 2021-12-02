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

            builder.Property(e => e.Organizers).HasMaxLength(500);
            builder.Property(e => e.CoOrganizers).HasMaxLength(500);

            builder.Property(e => e.Location).HasMaxLength(500);

            builder.Property(e => e.Number).HasMaxLength(500);

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.SubType)
                .WithMany()
                .HasForeignKey(e => e.SubTypeId);

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
