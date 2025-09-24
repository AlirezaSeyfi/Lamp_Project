using _0_FrameWork.Domain;
using BlogManagment.Application.Contract.Article;

namespace BlogManagment.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        Article GetWithCategory(long id);
        EditArticle GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
