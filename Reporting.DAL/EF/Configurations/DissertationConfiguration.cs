using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class DissertationConfiguration : AuditableEntityConfiguration<Dissertation>
    {
        public override void Configure(EntityTypeBuilder<Dissertation> builder)
        {
            base.Configure(builder);

            builder.ToTable("Dissertations");

            builder.Property(e => e.PlaceOfWork).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Supervisor).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Specialty).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Topic).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Deadline).HasMaxLength(500);

            builder.Property(e => e.DefensePlace).IsRequired().HasMaxLength(500);

            builder.Property(e => e.AuthorName).HasMaxLength(500);

            builder.HasOne(e => e.Author)
                .WithMany()
                .HasForeignKey(e => e.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
