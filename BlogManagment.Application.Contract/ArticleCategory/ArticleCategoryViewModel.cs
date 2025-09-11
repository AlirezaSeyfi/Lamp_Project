namespace BlogManagment.Application.Contract.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }
        public int ShowOrder { get; set; }
        public string CreationDate { get; set; }
        public long ArtilcesCount { get; set; }
    }
}
