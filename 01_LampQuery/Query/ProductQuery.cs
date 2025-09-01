using _0_FrameWork.Application;
using _01_LampQuery.Contract.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductQueryModel GetDetails(string slug)
        {
            var inventoryDict = _inventoryContext.Inventory
                                 .Select(x => new { x.ProductId, x.UnitPrice })
                                  .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .GroupBy(x => x.ProductId)
                .Select(g => g.First())
                .ToDictionary(x => x.ProductId, x => x);

            var product = _shopContext.Products
                .Include(x => x.Category)
                    .Select(x => new ProductQueryModel()
                    {
                        Id = x.Id,
                        Category = x.Category.Name,
                        Name = x.Name,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        ShortDescription = x.ShortDescription,
                        Slug = x.Slug,
                    }).FirstOrDefault(x => x.Slug == slug);

            inventoryDict.TryGetValue(product.Id, out var price);
            product.Price = price.ToMoney();

            if (discountsDict.TryGetValue(product.Id, out var discountRate))
            {
                product.DiscountRate = discountRate.DiscountRange;

                var discountAmount = Math.Round((price * discountRate.DiscountRange) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }

            return product;
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventoryDict = _inventoryContext.Inventory
                                .Select(x => new { x.ProductId, x.UnitPrice })
                                 .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .GroupBy(x => x.ProductId)
                .Select(g => g.First())
                .ToDictionary(x => x.ProductId, x => x); // کل آبجکت تخفیف ذخیره میشه



            var products = _shopContext.Products.Include(x => x.Category)
                .Select(p => new ProductQueryModel()
                {
                    Id = p.Id,
                    Category = p.Category.Name,
                    Name = p.Name,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Slug = p.Slug
                }).OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                inventoryDict.TryGetValue(product.Id, out var price);
                product.Price = price.ToMoney();

                if (discountsDict.TryGetValue(product.Id, out var discountRate))
                {
                    product.DiscountRate = discountRate.DiscountRange;

                    var discountAmount = Math.Round((price * discountRate.DiscountRange) / 100);
                    product.PriceWithDiscount = (price - discountAmount).ToMoney();
                }
            }

            return products;
        }

        public List<ProductQueryModel> Search(string Value)
        {
            var inventoryDict = _inventoryContext.Inventory
                                 .Select(x => new { x.ProductId, x.UnitPrice })
                                  .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .GroupBy(x => x.ProductId)
                .Select(g => g.First())
                .ToDictionary(x => x.ProductId, x => x);


            var query = _shopContext.Products
                .Include(x => x.Category)
                    .Select(x => new ProductQueryModel()
                    {
                        Id = x.Id,
                        Category = x.Category.Name,
                        Name = x.Name,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        ShortDescription = x.ShortDescription,
                        Slug = x.Slug,
                    }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(Value))
                query = query.Where(x => x.Name.Contains(Value) || x.ShortDescription.Contains(Value));

            var products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in products)
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

            return products;
        }
    }
}
