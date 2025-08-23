using _0_FrameWork.Application;
using _0_FrameWork.Infrastructure;
using DiscountManagment.Application.Contract.CustomerDiscount;
using DiscountManagment.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;
using System.Linq.Expressions;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public void Create(CustomerDiscount entity)
        {
            _discountContext.Add(entity);
        }

        public bool Exists(Expression<Func<CustomerDiscount, bool>> expression)
        {
            return _discountContext.customerDiscounts.Any(expression);
        }

        public CustomerDiscount Get(long id)
        {
            return _discountContext.customerDiscounts.FirstOrDefault(x=>x.Id==id);
        }

        public List<CustomerDiscount> GetAll()
        {
            return _discountContext.customerDiscounts.ToList();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.customerDiscounts
                .Select(x => new EditCustomerDiscount()
                {
                    Id = x.Id,
                    DiscountRange = x.DiscountRange,
                    Reason = x.Reason,
                    ProductId = x.ProductId,
                    StartDate = x.StartDate.ToString(),
                    EndDate = x.EndDate.ToString()
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveChange()
        {
            _discountContext.SaveChanges();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
            var query = _discountContext.customerDiscounts.Select(x => new CustomerDiscountViewModel()
            {
                Id = x.Id,
                DiscountRange = x.DiscountRange,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                StartDateGr = x.StartDate,
                EndDateGr = x.EndDate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate=x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateGr >= searchModel.StartDate.ToGeorgianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.EndDateGr <= searchModel.EndDate.ToGeorgianDateTime());
            }

            var discounts = query.OrderByDescending(x=>x.Id).ToList();
            discounts.ForEach(d =>
               d.ProductName = products.FirstOrDefault(x => x.Id == d.ProductId)?.Name);

            return discounts;
        }
    }
}
