using Budget.Bll.DomainObjects;

namespace Budget.ViewModels.Workers
{
    class AddWorker : IWorker
    {
        public void Init(AddOrUpdateStorageViewModel viewModel) { }

        public void Save(AddOrUpdateStorageViewModel viewModel)
        {
            FinanceStorage storage = new FinanceStorage
            {
                Name = viewModel.Name
            };
            viewModel.BudgetObject.AddFinanceStorage(storage);
        }
    }
}