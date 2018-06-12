using Budget.Bll.DomainObjects;
using Budget.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Budget.ViewModels
{
    class StoragesViewModel : BindableBase
    {
        readonly BudgetObject budgetObject;

        public StoragesViewModel(BudgetObject budgetObject)
        {
            this.budgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));

            AddStorageCommand = new DelegateCommand(AddStorageExecute);
            UpdateStorageCommand = new DelegateCommand(UpdateStorageExecute);
            DeleteStorageCommand = new DelegateCommand(DeleteStorageExecute);

            AddStorageNotificationRequest = new InteractionRequest<IAddObjectNotification>();
            UpdateStorageNotificationRequest = new InteractionRequest<IUpdateObjectNotification<FinanceStorage>>();

            budgetObject.FinanceStoragesChanged += (s, e) => RaisePropertyChanged(nameof(Storages));
        }

        public IEnumerable<StorageViewModel> Storages
        {
            get { return budgetObject.GetFinanceStorage().Select(s => new StorageViewModel(s)); }
        }

        public StorageViewModel SelectedStorage { get; set; }

        public ICommand AddStorageCommand { get; }
        public ICommand UpdateStorageCommand { get; }
        public ICommand DeleteStorageCommand { get; }

        public InteractionRequest<IAddObjectNotification> AddStorageNotificationRequest { get; }
        public InteractionRequest<IUpdateObjectNotification<FinanceStorage>> UpdateStorageNotificationRequest { get; }

        private void AddStorageExecute()
        {
            AddStorageNotificationRequest.Raise(new AddObjectNotification("Добавление финансового хранения"));
        }

        private void UpdateStorageExecute()
        {
            if (SelectedStorage != null)
            {
                UpdateStorageNotificationRequest.Raise(new UpdateObjectNotification<FinanceStorage>("Редактирование финансового хранения", SelectedStorage.Storage));
            }
        }

        private void DeleteStorageExecute()
        {
            
        }
    }
}