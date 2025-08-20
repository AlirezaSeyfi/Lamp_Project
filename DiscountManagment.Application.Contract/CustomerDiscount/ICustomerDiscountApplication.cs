using _0_FrameWork.Application;

namespace DiscountManagment.Application.Contract.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Edit(EditCustomerDiscount command);
        List<CustomerDiscountViewModel> Search(CustomerDiscounSearchModel searchModel);
        EditCustomerDiscount GetDetails(long id);
    }
}
