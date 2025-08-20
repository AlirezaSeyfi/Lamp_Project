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
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<CustomerDiscount, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public CustomerDiscount Get(long id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDiscount> GetAll()
        {
            throw new NotImplementedException();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.customerDiscounts
                .Select(x => new EditCustomerDiscount()
                {
                    Id = id,
                    DiscountRange = x.DiscountRange,
                    Reason = x.Reason,
                    ProductId = x.ProductId,
                    StartDate = x.StartDate.ToFarsi(),
                    EndDate = x.EndDate.ToFarsi()
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscounSearchModel searchModel)
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
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                var startDate = DateTime.Now;
                query = query.Where(x => x.StartDateGr > startDate);
            }

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                var endDate = DateTime.Now;
                query = query.Where(x => x.EndDateGr < endDate);
            }

            var discounts = query.OrderByDescending(x=>x.Id).ToList();
            discounts.ForEach(d =>
               d.ProductName = products.FirstOrDefault(x => x.Id == d.ProductId)?.Name);

            return discounts;
        }
    }
}
