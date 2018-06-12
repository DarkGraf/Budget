using Budget.Bll.DomainObjects;
using Budget.Notifications;
using Budget.ViewModels.Workers;
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

            AddStorageNotificationRequest = new InteractionRequest<IAddOrUpdateObjectNotification>();
            UpdateStorageNotificationRequest = new InteractionRequest<IAddOrUpdateObjectNotification>();

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

        public InteractionRequest<IAddOrUpdateObjectNotification> AddStorageNotificationRequest { get; }
        public InteractionRequest<IAddOrUpdateObjectNotification> UpdateStorageNotificationRequest { get; }

        private void AddStorageExecute()
        {
            AddStorageNotificationRequest
                .Raise(new AddOrUpdateObjectNotification("Добавление финансового хранения", new AddWorker()));
        }

        private void UpdateStorageExecute()
        {
            if (SelectedStorage != null)
            {
                UpdateStorageNotificationRequest
                    .Raise(new AddOrUpdateObjectNotification("Редактирование финансового хранения", new UpdateWorker(SelectedStorage.Storage)));
            }
        }

        private void DeleteStorageExecute()
        {
            
        }
    }
}