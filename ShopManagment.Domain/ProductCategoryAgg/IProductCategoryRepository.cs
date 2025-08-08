using ShopManagment.Application.Contracts.ProductCategory;
using System.Linq.Expressions;

namespace ShopManagment.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository
    {
        void Create(ProductCategory entity);
        ProductCategory Get(long id);
        List<ProductCategory> GetAll();
        bool Exist(Expression<Func<ProductCategory, bool>> expression);
        void SaveChanges();
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
