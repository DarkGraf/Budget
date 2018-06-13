using Budget.Bll.DomainObjects;

namespace Budget.ViewModels.Workers
{
    class AddWorker : IWorker
    {
        public void Init(IStorageViewModel viewModel) { }

        public void Save(IStorageViewModel viewModel)
        {
            FinanceStorage storage = new FinanceStorage
            {
                Name = viewModel.Name
            };
            viewModel.BudgetObject.AddFinanceStorage(storage);
            viewModel.Name = string.Empty;
        }
    }
}