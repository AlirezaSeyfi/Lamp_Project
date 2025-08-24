using _0_FrameWork.Domain;
using DiscountManagment.Application.Contract.ColleagueDiscount;

namespace DiscountManagment.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository:IRepository<long , ColleagueDiscount>
    {
        EditColleagueDiscount GetDetails(long id);
        List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel);
    }
}
