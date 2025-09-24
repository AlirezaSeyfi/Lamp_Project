namespace BlogManagment.Application.Contract.Article
{
    public class EditArticle : CreateArticle
    {
        public long Id { get; set; }
        public string ExistingPicturePath { get; set; }
    }
}
