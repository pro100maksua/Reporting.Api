using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class RoleConfiguration : ComboboxItemConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.ToTable("Roles");

            builder.HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity(e => e.ToTable("UserRoles"));
        }
    }
}
