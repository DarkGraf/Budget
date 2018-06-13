namespace Budget.ViewModels.Workers
{
    interface IWorker
    {
        void Init(IStorageViewModel viewModel);
        void Save(IStorageViewModel viewModel);
    }
}