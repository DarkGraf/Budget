using Budget.Bll.DomainObjects;
using Budget.Notifications;
using Budget.ViewModels.Workers;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace Budget.ViewModels
{
    class AddOrUpdateStorageViewModel : BindableBase, IInteractionRequestAware
    {
        private IAddOrUpdateObjectNotification notification;
        private IWorker worker;

        private string name;

        public AddOrUpdateStorageViewModel(BudgetObject budgetObject)
        {
            this.BudgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));

            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        #region IInteractionRequestAware

        public INotification Notification
        {
            get { return notification; }
            set
            {
                notification = value as IAddOrUpdateObjectNotification;
                if (notification == null)
                {
                    throw new InvalidOperationException();
                }
                worker = notification.Worker;
                worker.Init(this);
            }
        }

        public BudgetObject BudgetObject { get; }

        public Action FinishInteraction { get; set; }

        #endregion

        /// <summary>
        /// Идентификатор хранения, для обновления. В UI не используется.
        /// </summary>
        public long Id { get; set; }

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        private void OkExecute()
        {
            worker.Save(this);
            FinishInteraction();
            ClearFields();
        }

        private void CancelExecute()
        {
            FinishInteraction();
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            worker = null;
        }
    }
}
