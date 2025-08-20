using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagment.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(750).IsRequired(false); ;
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired(false); ;
            builder.Property(x => x.PictureAlt).HasMaxLength(255).IsRequired(false); ;
            builder.Property(x => x.PictureTitle).HasMaxLength(255).IsRequired(false); ;
            builder.Property(x => x.KeyWord).HasMaxLength(80).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(750).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(300).IsRequired();

            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);

        }
    }
}
