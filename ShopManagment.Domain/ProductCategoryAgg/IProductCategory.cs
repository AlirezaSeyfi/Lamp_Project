namespace ShopManagment.Domain.ProductCategoryAgg
{
    public interface IProductCategory
    {
        void Create(ProductCategory entity);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
    }
}
