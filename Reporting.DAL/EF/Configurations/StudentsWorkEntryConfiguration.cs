using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class StudentsWorkEntryConfiguration : AuditableEntityConfiguration<StudentsWorkEntry>
    {
        public override void Configure(EntityTypeBuilder<StudentsWorkEntry> builder)
        {
            base.Configure(builder);

            builder.ToTable("StudentsWorkEntries");

            builder.Property(e => e.Name).HasMaxLength(500);

            builder.Property(e => e.Group).HasMaxLength(500);
            builder.Property(e => e.Specialty).HasMaxLength(500);

            builder.Property(e => e.ScientificWorkName).HasMaxLength(500);

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ScientificWorkType)
                .WithMany()
                .HasForeignKey(e => e.ScientificWorkTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Teacher)
                .WithMany()
                .HasForeignKey(e => e.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
