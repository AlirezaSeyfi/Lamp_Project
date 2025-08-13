using _0_FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProduct GetDetail(long id)
        {
            return _context.Products
                .Where(x => x.Id == id)
                .Select(x => new EditProduct
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Slug = x.Slug,
                    CategoryId = x.CategoryId,
                    Description = x.Description,
                    KeyWords = x.KeyWords,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    UnitPrice = x.UnitPrice
                })
                .SingleOrDefault();
        }


        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                CategoryId = x.Category.Id,
                Code = x.Code,
                Picture = x.Picture,
                UnitPrice = x.UnitPrice,
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code == searchModel.Code);

            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
