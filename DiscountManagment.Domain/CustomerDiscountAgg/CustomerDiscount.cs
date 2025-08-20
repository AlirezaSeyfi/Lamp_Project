using _0_FrameWork.Domain;

namespace DiscountManagment.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount:EntityBase
    {
        public long ProductId{ get; private set; }
        public int DiscountRange { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public CustomerDiscount(long productId, int discountRange, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRange = discountRange;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Edit(long productId, int discountRange, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRange = discountRange;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
