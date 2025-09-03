namespace _01_LampQuery.Contract.Product
{
    public class ProductPictureQueryModel 
    {
        public string ProductName { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public bool IsRemoved { get;  set; }
        public long ProductId { get; set; }
    }
}
