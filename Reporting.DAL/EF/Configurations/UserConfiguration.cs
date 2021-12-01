using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class UserConfiguration : AuditableEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("Users");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Email).IsRequired().HasMaxLength(500);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Degree).HasMaxLength(500);
            builder.Property(e => e.AcademicStatus).HasMaxLength(500);
            builder.Property(e => e.Position).HasMaxLength(500);

            builder.Property(e => e.IeeeXploreAuthorName).HasMaxLength(500);

            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId);
        }
    }
}
