using DiscountManagment.Application.Contract.ColleagueDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagment.Application.Contracts.Product;

namespace ServiceHost.Areas.Admin.Pages.Discounts.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        public ColleagueDiscountSearchModel SearchModel;
        public List<ColleagueDiscountViewModel> colleagueDiscounts;
        public SelectList Products;

        private readonly IProductApplication _productApplication;
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;

        public IndexModel(IProductApplication productApplication, IColleagueDiscountApplication colleagueDiscountApplication)
        {
            _productApplication = productApplication;
            _colleagueDiscountApplication = colleagueDiscountApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            colleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount()
            {
                Products = _productApplication.GetProducts()
            };

            return Partial("Create", command);
        }

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var customerDiscount = _colleagueDiscountApplication.GetDetails(id);
            customerDiscount.Products = _productApplication.GetProducts();

            return Partial("Edit", customerDiscount);
        }

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _colleagueDiscountApplication.Remove(id);
            if (result.IsSuccedded)
                return RedirectToPage(new { area = "Admin" });

            Message = result.Message;
            return RedirectToPage(new { area = "Admin" });

            //_colleagueDiscountApplication.Remove(id);
            //return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var result = _colleagueDiscountApplication.Restore(id);
            if (result.IsSuccedded)
                return RedirectToPage(new { area = "Admin" });

            Message = result.Message;
            return RedirectToPage(new { area = "Admin" });
        }
    }
}
