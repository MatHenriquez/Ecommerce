using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DataAccess.Configuration
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.Status)
                .IsRequired();
        }
    }
}
