namespace ShopManagment.Application.Contracts.Slide
{
    public class EditSlide : CreateSlide
    {
        public long Id { get; set; }
        public string ExistingPicturePath { get; set; }
    }

}
