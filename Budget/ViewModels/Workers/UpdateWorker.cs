using Budget.Bll.DomainObjects;
using System;

namespace Budget.ViewModels.Workers
{
    class UpdateWorker : IWorker
    {
        readonly FinanceStorage storage;

        public UpdateWorker(FinanceStorage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void Init(AddOrUpdateStorageViewModel viewModel)
        {
            viewModel.Id = storage.Id;
            viewModel.Name = storage.Name;
        }

        public void Save(AddOrUpdateStorageViewModel viewModel)
        {
            FinanceStorage storage = new FinanceStorage
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };
            viewModel.BudgetObject.UpdateFinanceStorage(storage);
        }
    }
}