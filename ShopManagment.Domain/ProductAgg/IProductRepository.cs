using _0_FrameWork.Domain;
using ShopManagment.Application.Contracts.Product;

namespace ShopManagment.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long, Product>
    {
        EditProduct GetDetail(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel); 
    }
}
