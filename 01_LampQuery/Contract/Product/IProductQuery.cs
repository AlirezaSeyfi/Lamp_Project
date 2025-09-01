namespace _01_LampQuery.Contract.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetDetails(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
