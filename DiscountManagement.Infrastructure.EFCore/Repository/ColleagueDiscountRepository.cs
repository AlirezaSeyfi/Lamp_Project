using _0_FrameWork.Application;
using _0_FrameWork.Infrastructure;
using DiscountManagment.Application.Contract.ColleagueDiscount;
using DiscountManagment.Domain.ColleagueDiscountAgg;
using ShopManagement.Infrastructure.EFCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public ColleagueDiscountRepository(DiscountContext discountContext, ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _discountContext.colleagueDiscounts.Select(x => new EditColleagueDiscount()
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                ProductId = x.ProductId,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            var query = _discountContext.colleagueDiscounts.Select(_x => new ColleagueDiscountViewModel()
            {
                Id = _x.Id,
                CreationDate = _x.CreationDate.ToFarsi(),
                DiscountRate = _x.DiscountRate,
                ProductId = _x.ProductId,
                IsRemoved = _x.IsRemoved,
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var discounts = query.OrderByDescending(x => x.Id).ToList();
            discounts.ForEach(d => d.Product = products.FirstOrDefault(x => x.Id == d.ProductId)?.Name);

            return discounts;
        }
    }
}
