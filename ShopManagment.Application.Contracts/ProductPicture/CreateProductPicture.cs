using Microsoft.AspNetCore.Http;
using ShopManagment.Application.Contracts.Product;

namespace ShopManagment.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }


}
