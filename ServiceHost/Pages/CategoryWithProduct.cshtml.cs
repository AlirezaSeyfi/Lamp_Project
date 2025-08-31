using _01_LampQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class CategoryWithProductModel : PageModel
    {
        public ProductCategoryQueryModel Category;

        private readonly IProductCategoryQuery _productCategoryQuery;
        public CategoryWithProductModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public void OnGet(string id)
        {
            Category = _productCategoryQuery.GetProductCategoryWithProductBySlug(id);
        }
    }
}
