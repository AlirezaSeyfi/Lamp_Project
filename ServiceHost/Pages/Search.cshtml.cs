using _01_LampQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class SearchModel : PageModel
    {
        public List<ProductQueryModel> Products;
        private readonly IProductQuery _productQuery;

        public SearchModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public void OnGet(string value)
        {
            Products = _productQuery.Search(value);
        }
    }
}
