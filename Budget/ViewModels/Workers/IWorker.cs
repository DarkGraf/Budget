namespace Budget.ViewModels.Workers
{
    interface IWorker
    {
        void Init(AddOrUpdateStorageViewModel viewModel);
        void Save(AddOrUpdateStorageViewModel viewModel);
    }
}