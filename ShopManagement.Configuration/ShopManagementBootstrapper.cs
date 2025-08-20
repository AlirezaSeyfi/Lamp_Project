using _0_FrameWork.Application;
using _01_LampQuery.Contract.ProductCategory;
using _01_LampQuery.Contract.Slide;
using _01_LampQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EFCore.Repository;
using ShopManagment.Application;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.IProductPictureRepository;
using ShopManagment.Domain.ProductAgg;
using ShopManagment.Domain.ProductCategoryAgg;
using ShopManagment.Domain.ProductPictureAgg;
using ShopManagment.Domain.SliderAgg;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductApplication, ProductApplication>();

            services.AddTransient<IProductPictureRepository, ProductPictureRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISlideApplication, SlideApplication>();
            services.AddTransient<ISlideRepository, SlideRepository>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }
    }
}