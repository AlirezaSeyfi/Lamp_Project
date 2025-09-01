using _01_LampQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        public ProductQueryModel Product;
        private readonly IProductQuery _productQuery;

        public ProductModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string slug)
        {
            Product=_productQuery.GetDetails(slug);
        }
    }
}
