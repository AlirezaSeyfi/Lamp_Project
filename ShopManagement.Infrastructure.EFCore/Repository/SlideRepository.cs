using _0_FrameWork.Application;
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
                Id = x.Id,
                BtnText = x.BtnText,
                Heading = x.Heading,
                Title = x.Title,
                Text = x.Text,
                ExistingPicturePath=x.Picture,
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
                ExistingPicturePath = x.Picture,
                Title = x.Title,
                IsRemoved = x.IsRemoved,
                CreationDate = x.CreationDate.ToFarsi(),
            }).OrderByDescending(x => x.Id)
            .ToList();
        }
    }
}
