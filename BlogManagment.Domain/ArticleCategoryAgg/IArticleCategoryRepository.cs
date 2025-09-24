using _0_FrameWork.Domain;
using BlogManagment.Application.Contract.ArticleCategory;

namespace BlogManagment.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        string GetSlugBy(long id);
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);

    }
}
