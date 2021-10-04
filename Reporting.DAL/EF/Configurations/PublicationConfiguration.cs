using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reporting.Domain.Entities;

namespace Reporting.DAL.EF.Configurations
{
    public class PublicationConfiguration : AuditableEntityConfiguration<Publication>
    {
        public override void Configure(EntityTypeBuilder<Publication> builder)
        {
            base.Configure(builder);

            builder.ToTable("Publications");

            builder.Property(e => e.Title).IsRequired().HasMaxLength(500);
            builder.Property(e => e.PublicationTitle).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Authors).IsRequired().HasMaxLength(500);

            builder.Property(e => e.Doi).HasMaxLength(500);
            builder.Property(e => e.Publisher).HasMaxLength(500);
            builder.Property(e => e.Isbn).HasMaxLength(500);
            builder.Property(e => e.Abstract).HasMaxLength(2000);
            builder.Property(e => e.ArticleNumber).HasMaxLength(500);
            builder.Property(e => e.PdfUrl).HasMaxLength(500);
            builder.Property(e => e.HtmlUrl).HasMaxLength(500);
            builder.Property(e => e.ConferenceLocation).HasMaxLength(500);

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
