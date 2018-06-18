using Budget.Bll.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class StoragesViewModel : ObjectListViewModel<StorageViewModel>
    {
        readonly BudgetObject budgetObject;

        public StoragesViewModel(BudgetObject budgetObject)
        {
            this.budgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));
            budgetObject.FinanceStoragesChanged += (s, e) => RaisePropertyChanged(nameof(Items));
        }

        protected override IEnumerable<StorageViewModel> GetItems()
        {
            return budgetObject.GetFinanceStorage().Select(s => new StorageViewModel(s));
        }

        protected override void AddSaveItem(StorageViewModel item)
        {
            budgetObject.AddFinanceStorage(item.Storage);
        }

        protected override void EditSaveItem(StorageViewModel item)
        {
            budgetObject.UpdateFinanceStorage(item.Storage);
        }

        protected override void DeleteSaveItem(StorageViewModel item)
        {
            budgetObject.DeleteFinanceStorage(item.Storage);
        }
    }
}