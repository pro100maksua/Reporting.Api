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

            builder.Property(e => e.FirstName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.LastName).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Email).IsRequired().HasMaxLength(250);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(250);

            builder.Property(e => e.ScopusAuthorName).HasMaxLength(250);

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId);
        }
    }
}
