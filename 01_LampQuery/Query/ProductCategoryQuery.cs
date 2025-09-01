using _0_FrameWork.Application;
using _01_LampQuery.Contract.Product;
using _01_LampQuery.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using ShopManagment.Domain.ProductAgg;

namespace _01_LampQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories
                .Include(x => x.Products)
                .Select(x => new ProductCategoryQueryModel()
                {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Products = MapProducts(x.Products)
                })
                .ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProduct()
        {
            var inventoryDict = _inventoryContext.Inventory
                                .Select(x => new { x.ProductId, x.UnitPrice })
                                 .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                                 .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                                 .GroupBy(x => x.ProductId)
                                 .Select(g => g.First()) // فقط اولین تخفیف هر محصول
                                 .ToDictionary(x => x.ProductId, x => x.DiscountRange);


            var categories = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                    .Select(x => new ProductCategoryQueryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Products = MapProducts(x.Products),
                        Slug = x.Slug,
                        Description = x.Description,
                        KeyWord = x.KeyWord,
                        MetaDescription = x.MetaDescription,
                    }).ToList();

            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    inventoryDict.TryGetValue(product.Id, out var price);
                    product.Price = price.ToMoney();

                    discountsDict.TryGetValue(product.Id, out var discountRate);
                    product.DiscountRate = discountRate;

                    var discountAmount = Math.Round((price * discountRate) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            return categories;
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products.Select(p => new ProductQueryModel()
            {
                Id = p.Id,
                Category = p.Category?.Name,
                Name = p.Name,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Slug = p.Slug,
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductBySlug(string slug)
        {
            var inventoryDict = _inventoryContext.Inventory
                                .Select(x => new { x.ProductId, x.UnitPrice })
                                 .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .GroupBy(x => x.ProductId)
                .Select(g => g.First())
                .ToDictionary(x => x.ProductId, x => x); // کل آبجکت تخفیف ذخیره میشه


            var category = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                    .Select(x => new ProductCategoryQueryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Products = MapProducts(x.Products),
                        Slug = x.Slug

                    }).FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products)
            {
                inventoryDict.TryGetValue(product.Id, out var price);
                product.Price = price.ToMoney();


                if (discountsDict.TryGetValue(product.Id, out var discountInfo))
                {
                    product.DiscountRate = discountInfo.DiscountRange;
                    product.DiscountExpireDate = discountInfo.EndDate.ToDiscountFormat();

                    var discountAmount = Math.Round((price * discountInfo.DiscountRange) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }

            }

            return category;
        }
    }
}
