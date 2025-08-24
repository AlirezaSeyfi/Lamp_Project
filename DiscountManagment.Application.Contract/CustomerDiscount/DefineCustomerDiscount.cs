using _0_FrameWork.Application;
using ShopManagment.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagment.Application.Contract.CustomerDiscount
{
    public class DefineCustomerDiscount
    {
        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک محصول انتخاب کنید")]
        public long ProductId { get;  set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRange { get;  set; }
        
        public string StartDate { get;  set; }
        public string EndDate { get;  set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Reason { get;  set; }
        public List<ProductViewModel> Products { get; set; }
    }

}
