using _0_FrameWork.Domain;
using ShopManagment.Domain.ProductAgg;

namespace ShopManagment.Domain.ProductPictureAgg
{
    public class ProductPicture : EntityBase
    {
        public string ProductName { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }
        public long ProductId { get; set; }
        public Product Product{ get; set; }
        public ProductPicture(string productName, long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductName = productName;
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }
        public void Edit(string productName, long productId, string picture, string pictureAlt, string pictureTitle)
        {
            ProductId = productId;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }
        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
