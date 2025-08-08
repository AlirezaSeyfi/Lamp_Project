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
            var operation = new OperationResult();

            if (_productCategoryRepository.Exist(x => x.Name == command.Name))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد");

            var slug = Slugify.GenerateSlug(command.Slug);
            var productCategory = new ProductCategory(command.Name, command.Description, command.Picture, command.PictureAlt, command.Title, command.KeyWord, command.MetaDescription, slug);
            _productCategoryRepository.Create(productCategory);
            _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();

            var productCategory = _productCategoryRepository.Get(command.Id);
            if (productCategory == null)
                return operation.Failed("رکورد با اطلاعات درخواست شده یافت نشد");

            if (_productCategoryRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed("امکان ثبت رکورد تکراری وجود ندارد");

            var slug = Slugify.GenerateSlug(command.Slug);
            productCategory.Edit(command.Name, command.Description, command.Picture, command.PictureAlt, command.Title, command.KeyWord, command.MetaDescription, slug); ;
            _productCategoryRepository.SaveChanges();

            return operation.Succedded();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetails(id);
        }
    }
}
