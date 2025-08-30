using _0_FrameWork.Application;
using _01_LampQuery.Contract.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryMangement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventoryDict = _inventoryContext.Inventory
                                .Select(x => new { x.ProductId, x.UnitPrice })
                                 .ToDictionary(x => x.ProductId, x => x.UnitPrice);

            var discountsDict = _discountContext.customerDiscounts
                                 .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                                 .Select(x => new { x.ProductId, x.DiscountRange })
                                 .ToDictionary(x => x.ProductId, x => x.DiscountRange);

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

                discountsDict.TryGetValue(product.Id, out var discountRate);
                product.DiscountRate = discountRate;

                var discountAmount = Math.Round((price * discountRate) / 100);
                product.PriceWithDiscount = (price - discountAmount).ToMoney();
            }

            return products;
        }
    }
}
