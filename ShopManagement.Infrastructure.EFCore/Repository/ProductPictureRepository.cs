using _0_FrameWork.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagment.Application.Contracts.ProductPicture;
using ShopManagment.Domain.IProductPictureRepository;
using ShopManagment.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(x => new EditProductPicture
            {
                Id = x.Id,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId,
                ExistingPicturePath=x.Picture
            }).FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _context.ProductPictures.FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(x => x.Product)
                                .Select(x => new ProductPictureViewModel()
                                {
                                    Id = x.Id,
                                    Product = x.Product.Name,
                                    CreationDate = x.CreationDate.ToString(),
                                    Picture = x.Picture,
                                    ProductId = x.ProductId,
                                    IsRemoved = x.IsRemoved
                                });

            if (searchModel.ProductId != 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            return query.OrderByDescending(x => x.Id).ToList();
            ;
        }
    }
}
