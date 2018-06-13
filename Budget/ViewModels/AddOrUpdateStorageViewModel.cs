using Budget.Bll.DomainObjects;

namespace Budget.ViewModels
{
    class AddOrUpdateStorageViewModel : StorageViewModelBase
    {
        public AddOrUpdateStorageViewModel(BudgetObject budgetObject)
            : base(budgetObject) { }
    }
}
