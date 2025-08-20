using _0_FrameWork.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagment.Application.Contracts.ProductPicture
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
        public string ExistingPicturePath { get; set; }
        public IFormFile PictureEdit { get; set; }
    }


}
