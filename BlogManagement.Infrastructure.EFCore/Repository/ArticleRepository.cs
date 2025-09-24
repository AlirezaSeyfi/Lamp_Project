using _0_FrameWork.Application;
using _0_FrameWork.Infrastructure;
using BlogManagment.Application.Contract.Article;
using BlogManagment.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleRepository : RepositoryBase<long, Article>, IArticleRepository
    {
        private readonly BlogContext _blogContext;

        public ArticleRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public EditArticle GetDetails(long id)
        {
            return _blogContext.Articles
                                 .Select(x => new EditArticle
                                 {
                                     Id = x.Id,
                                     Title = x.Title,
                                     CanonicalAddress = x.CanonicalAddress,
                                     CategoryId = x.CategoryId,
                                     Description = x.Description,
                                     KeyWords = x.KeyWords,
                                     MetaDescription = x.MetaDescription,
                                     PictureAlt = x.PictureAlt,
                                     PictureTitle = x.PictureTitle,
                                     PublishDate = x.PublishDate.ToString(),
                                     ShortDescription = x.ShortDescription,
                                     Slug = x.Slug
                                 })
                                 .FirstOrDefault(x => x.Id == id);
        }

        public Article GetWithCategory(long id)
        {
            return _blogContext.Articles.Include(x => x.ArticleCategory).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            var query = _blogContext.Articles.Select(x =>
            new ArticleViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Category = x.ArticleCategory.Name,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription,
            });

            if (!string.IsNullOrEmpty(searchModel.Title))
                query = query.Where(x => x.Title.Contains(searchModel.Title));

            if (searchModel.CategoryId > 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

    }
}
