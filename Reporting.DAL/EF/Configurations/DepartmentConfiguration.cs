using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public virtual void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);

            builder.HasOne(e => e.Faculty)
                .WithMany(e => e.Departments)
                .HasForeignKey(e => e.FacultyId);
        }
    }
}
