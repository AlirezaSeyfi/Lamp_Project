using _0_FrameWork.Infrastructure;
using ShopManagment.Application.Contracts.Slide;
using ShopManagment.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _Context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _Context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _Context.Slides.Select(x => new EditSlide()
            {
                Id = id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title,
                Text = x.Text,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle

            }).FirstOrDefault(x => x.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _Context.Slides.Select(x => new SlideViewModel()
            {
                Id = x.Id,
                Heading = x.Heading,
                Picture = x.Picture,
                Title = x.Title
            }).OrderByDescending(x => x.Id)
            .ToList();
        }
    }
}
