using _01_LampQuery.Contract.Slide;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel()
                {
                    BtnText = x.Text,
                    Heading = x.Heading,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                    Title = x.Title
                })
                .ToList();
        }
    }
}
