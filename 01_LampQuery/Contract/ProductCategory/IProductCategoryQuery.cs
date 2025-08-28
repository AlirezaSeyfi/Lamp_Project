using _01_LampQuery.Contract.Product;

namespace _01_LampQuery.Contract.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProduct();
    }
}
