using _0_FrameWork.Application;
using ShopManagment.Application.Contracts.ProductCategory;
using ShopManagment.Domain.ProductCategoryAgg;

namespace ShopManagment.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public OperationResult Create(CreateProductCategory command)
        {
            string fileName = null;

            if (command.Picture != null && command.Picture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder); // اطمینان از وجود پوشه

                fileName = Guid.NewGuid() + Path.GetExtension(command.Picture.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    command.Picture.CopyTo(stream);
                }
            }

            var operation = new OperationResult();

            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(َApplicationMessages.RecordNotFound);

            var slug = Slugify.GenerateSlug(command.Slug);
            var productCategory = new ProductCategory(command.Name, command.Description, fileName, command.PictureAlt, command.PictureTitle, command.KeyWord, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChange();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {

            string fileName = null;

            if (command.Picture != null && command.Picture.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(uploadsFolder); // اطمینان از وجود پوشه

                fileName = Guid.NewGuid() + Path.GetExtension(command.Picture.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    command.Picture.CopyTo(stream);
                }
            }
            else
            {
                fileName = command.ExistingPicturePath;
            }

            var operation = new OperationResult();

            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = Slugify.GenerateSlug(command.Slug);
            productCategory.Edit(command.Name, command.Description, fileName, command.PictureAlt, command.PictureTitle, command.KeyWord, command.MetaDescription, slug); ;
            _productCategoryRepository.SaveChange();

            return operation.Succedded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);

            #region Code
            //var query = _productCategoryRepository.GetAll();
            //return query
            //    .Where(x => string.IsNullOrEmpty(searchModel.Name) || x.Name.Contains(searchModel.Name))
            //    .Select(x => new ProductCategoryViewModel
            //    {
            //        Id = x.Id,
            //        Name = x.Name,
            //    }).ToList();
            #endregion

        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);

            #region Code
            //var productCategory = _productCategoryRepository.Get(id);
            //return new EditProductCategory
            //{
            //    Id = productCategory.Id,
            //    Name = productCategory.Name,
            //    Description = productCategory.Description,
            //    Picture = productCategory.Picture,
            //    PictureAlt = productCategory.PictureAlt,
            //    Title = productCategory.PictureTitle,
            //    MetaDescription = productCategory.MetaDescription,
            //    KeyWord = productCategory.KeyWord,
            //    Slug = productCategory.Slug
            //};
            #endregion
        }
    }
}
