using _0_FrameWork.Domain;
using DiscountManagment.Application.Contract.CustomerDiscount;

namespace DiscountManagment.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository:IRepository<long,CustomerDiscount>
    {
        List<CustomerDiscountViewModel> Search(CustomerDiscounSearchModel searchModel);
        EditCustomerDiscount GetDetails(long id);
    }
}
