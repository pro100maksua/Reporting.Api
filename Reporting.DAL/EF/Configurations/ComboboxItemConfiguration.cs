using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class ComboboxItemConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : ComboboxItem
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(250);
        }
    }
}
