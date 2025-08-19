using _0_FrameWork.Application;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.IProductPictureRepository;
using ShopManagment.Domain.ProductAgg;
using ShopManagment.Domain.ProductPictureAgg;

namespace ShopManagment.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductRepository productRepository, IProductPictureRepository productPictureRepository, IFileUploader fileUploader)
        {
            _productRepository = productRepository;
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateProductPicture command)
        {
            var operationResult = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            var path = $"{"ProductPictures"}//{product.Category.Slug}//{product.Slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            var productPicture = new ProductPicture(product.Name, command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.Create(productPicture);
            _productPictureRepository.SaveChange();
            return operationResult.Succedded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetDetail(command.ProductId);
            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            string fileName = null;

            if (command.Picture != null && command.Picture.Length > 0)
            {
                var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
                var picturePath = _fileUploader.Upload(command.Picture, path);
            }
            else
            {
                fileName = command.ExistingPicturePath;
            }

            productPicture.Edit(product.Name, command.ProductId, fileName, command.PictureAlt, command.PictureTitle);
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Remove();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);
            if (productPicture == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            productPicture.Restore();
            _productPictureRepository.SaveChange();
            return operation.Succedded();
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
