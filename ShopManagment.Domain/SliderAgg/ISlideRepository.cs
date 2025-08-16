using _0_FrameWork.Domain;
using ShopManagment.Application.Contracts.Slide;

namespace ShopManagment.Domain.SliderAgg
{
    public interface ISlideRepository : IRepository<long, Slide>
    {
        EditSlide GetDetails(long id);
        List<SlideViewModel> GetList();
    }
}
