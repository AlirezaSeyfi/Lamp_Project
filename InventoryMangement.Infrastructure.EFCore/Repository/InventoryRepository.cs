using _0_FrameWork.Application;
using _0_FrameWork.Infrastructure;
using InventoryManagment.Application.Contract.Inventory;
using InventoryManagment.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryMangement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;

        public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public Inventory GetBy(long productId)
        {
            return _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public EditInventory GetDetails(long id)
        {
            return _inventoryContext.Inventory.Select(x => new EditInventory()
            {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            return inventory.inventoryOperations.Select(x => new InventoryOperationViewModel()
            {
                Id= x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Operation = x.Operation,    
                OperationDate=x.OperationDate.ToFarsi(),
                Operator="مدیر سیستم",
                OperatorId=x.OperatorId,
                OrderId=x.OrderId,
            }).ToList();

        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            var product = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();

            var query = _inventoryContext.Inventory.Select(x => new InventoryViewModel()
            {
                Id = x.Id,
                UnitPrice = x.UnitPrice,
                ProductId = x.ProductId,
                InStock = x.InStock,
                CreationDate = x.CreationDate.ToFarsi(),
                CurrentCount = x.CalculateCurrentCount()
            });


            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (searchModel.InStock)
                query = query.Where(x => !x.InStock);

            var inventory = query.OrderByDescending(x => x.Id).ToList();

            inventory.ForEach(item =>
                item.Product = product.FirstOrDefault(x => x.Id == item.ProductId)?.Name
            );

            return inventory;
        }
    }
}
