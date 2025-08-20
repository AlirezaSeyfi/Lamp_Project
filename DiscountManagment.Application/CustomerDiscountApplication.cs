using _0_FrameWork.Application;
using DiscountManagment.Application.Contract.CustomerDiscount;
using DiscountManagment.Domain.CustomerDiscountAgg;

namespace DiscountManagment.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            throw new NotImplementedException();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscounSearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
