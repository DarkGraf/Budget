using Budget.Bll.DomainObjects;
using System;

namespace Budget.ViewModels
{
    class DeleteStorageViewModel : StorageViewModelBase
    {
        public DeleteStorageViewModel(BudgetObject budgetObject)
            : base(budgetObject) { }

        protected override void OnNameChanged(object sender, EventArgs e)
        {
            base.OnNameChanged(sender, e);

            Message = $"Удалить \"{Name}\"?";
            RaisePropertyChanged(nameof(Message));
        }

        public string Message { get; private set; }
    }
}