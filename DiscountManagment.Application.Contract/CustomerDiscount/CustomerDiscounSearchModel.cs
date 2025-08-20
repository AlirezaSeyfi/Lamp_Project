namespace DiscountManagment.Application.Contract.CustomerDiscount
{
    public class CustomerDiscounSearchModel
    {
        public long ProductId { get; set; }
        public long ProductName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

}
