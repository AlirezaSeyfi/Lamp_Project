using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;
using ShopManagment.Application.Contracts.Product;
using System.ComponentModel.DataAnnotations;

namespace ShopManagment.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, int.MaxValue, ErrorMessage = "لطفاً یک محصول انتخاب کنید")]
        public long ProductId { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ProductName { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }


}
