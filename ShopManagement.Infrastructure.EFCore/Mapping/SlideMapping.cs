using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagment.Domain.ProductCategoryAgg;
using ShopManagment.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class SlideMapping : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.ToTable("Slide");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired(false); ;
            builder.Property(x => x.Heading).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired(false); ;
            builder.Property(x => x.Text).HasMaxLength(255).IsRequired(false); ;
            builder.Property(x => x.BtnText).HasMaxLength(100).IsRequired();
        }
    }
}
