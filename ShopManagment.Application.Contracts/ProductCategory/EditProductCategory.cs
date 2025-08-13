using Microsoft.AspNetCore.Http;
namespace ShopManagment.Application.Contracts.ProductCategory
{
    public class EditProductCategory : CreateProductCategory
    {
        public long Id { get; set; }
        public string ExistingPicturePath { get; set; }
    }
}
