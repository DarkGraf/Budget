using Budget.Bll.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class OperationsViewModel : ObjectListViewModel<OperationViewModel>
    {
        readonly BudgetObject budgetObject;

        public OperationsViewModel(BudgetObject budgetObject)
        {
            this.budgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));
            budgetObject.FinanceOperationChanged += (s, e) => RaisePropertyChanged(nameof(Items));
        }

        protected override IEnumerable<OperationViewModel> GetItems()
        {
            return budgetObject.GetFinanceOperations().Select(o => new OperationViewModel(o));
        }

        protected override void AddItem(OperationViewModel item)
        {
            budgetObject.AddFinanceOperation(item.Object);
        }

        protected override void EditItem(OperationViewModel item)
        {
            budgetObject.UpdateFinanceOperation(item.Object);
        }

        protected override void DeleteItem(OperationViewModel item)
        {
            budgetObject.DeleteFinanceOperation(item.Object);
        }
    }
}