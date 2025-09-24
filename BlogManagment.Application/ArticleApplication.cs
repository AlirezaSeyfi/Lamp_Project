using _0_FrameWork.Application;
using BlogManagment.Application.Contract.Article;
using BlogManagment.Domain.ArticleAgg;
using BlogManagment.Domain.ArticleCategoryAgg;

namespace BlogManagment.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;

        public ArticleApplication(IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.GenerateSlug();
            var categorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"{categorySlug}/{slug}";
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var pictureName = _fileUploader.Upload(command.Picture, path);

            var article = new Article(command.Title, command.ShortDescription, command.Description, pictureName, command.PictureAlt, command.PictureTitle, publishDate, slug, command.MetaDescription, command.KeyWords, command.CanonicalAddress, command.CategoryId);

            _articleRepository.Create(article);
            _articleRepository.SaveChange();

            return operation.Succedded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            var article = _articleRepository.GetWithCategory(command.Id);

            if (article == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            string fileName = null;
            var path = "";
            var slug = command.Slug.GenerateSlug();
            if (command.Picture != null && command.Picture.Length > 0)
            {
                path = $"{article.ArticleCategory.Slug}/{slug}";
                fileName = _fileUploader.Upload(command.Picture, path);
            }
            else
            {
                fileName = command.ExistingPicturePath;
            }
            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var pictureName = _fileUploader.Upload(command.Picture, path);

            article.Edit(command.Title, command.ShortDescription, command.Description, pictureName, command.PictureAlt, command.PictureTitle, publishDate, slug, command.MetaDescription, command.KeyWords, command.CanonicalAddress, command.CategoryId);
            _articleRepository.SaveChange();

            return operation.Succedded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
