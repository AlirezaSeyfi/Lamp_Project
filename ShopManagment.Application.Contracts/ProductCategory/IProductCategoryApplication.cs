namespace ShopManagment.Application.Contracts.ProductCategory
{
    public interface IProductCategoryApplication
    {
        void Create(CreateProductCategory command);
        void Edit(EditProductCategory command);
        ShopManagment.Domain.ProductCategoryAgg.ProductCategory GetDetails(long id);
    }
}
