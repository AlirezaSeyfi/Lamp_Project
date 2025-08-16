namespace ShopManagment.Application.Contracts.ProductPicture
{
    public class EditProductPicture : CreateProductPicture
    {
        public long Id { get; set; }
        public string ExistingPicturePath { get; set; }
    }


}
