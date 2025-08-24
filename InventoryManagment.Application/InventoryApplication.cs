using _0_FrameWork.Application;
using InventoryManagment.Application.Contract.Inventory;

namespace InventoryManagment.Application
{
    public class InventoryApplication : IInventoryApplication
    {

        public OperationResult Create(CreateInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Edit(EditInventory command)
        {
            throw new NotImplementedException();
        }

        public EditInventory GetDetails(long id)
        {
            throw new NotImplementedException();
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            throw new NotImplementedException();
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            throw new NotImplementedException();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            throw new NotImplementedException();
        }

        public List<InventoryViewModel> Search(InventorySearchModel searchModel)
        {
            throw new NotImplementedException();
        }
    }
}
