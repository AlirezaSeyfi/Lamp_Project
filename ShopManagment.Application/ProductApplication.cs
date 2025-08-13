using _0_FrameWork.Application;
using ShopManagment.Application.Contracts.Product;
using ShopManagment.Domain.ProductAgg;

namespace ShopManagment.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public OperationResult Create(CreateProduct command)
        {
            var operationResult = new OperationResult();
            if (_productRepository.Exists(x => x.Name == command.Name))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = Slugify.GenerateSlug(command.Slug);
            var product = new Product(command.Name, command.Code, command.UnitPrice, command.ShortDescription, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.CategoryId, slug, command.KeyWords, command.MetaDescription);
            _productRepository.Create(product);
            _productRepository.SaveChange();
            return operationResult.Succedded();

        }

        public OperationResult Edit(EditProduct command)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(command.Id);

            if (product == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = Slugify.GenerateSlug(command.Slug);
            product.Edit(command.Name, command.Code, command.UnitPrice, command.ShortDescription, command.Description, command.Picture, command.PictureAlt, command.PictureTitle, command.CategoryId, slug, command.KeyWords, command.MetaDescription);
            _productRepository.SaveChange();
            return operationResult.Succedded();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetail(id);
        }

        public OperationResult InStock(long id)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            product.InStock();
            _productRepository.SaveChange();
            return operationResult.Succedded();
        }

        public OperationResult NotInStock(long id)
        {
            var operationResult = new OperationResult();
            var product = _productRepository.Get(id);

            if (product == null)
                return operationResult.Failed(ApplicationMessages.RecordNotFound);

            product.NotInStock();
            _productRepository.SaveChange();
            return operationResult.Succedded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
