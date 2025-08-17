using Microsoft.AspNetCore.Http;

namespace ShopManagment.Application.Contracts.Slide
{
    public class CreateSlide
    {
        public IFormFile Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public string Text { get;  set; }
        public string BtnText { get;  set; }
    }

}
