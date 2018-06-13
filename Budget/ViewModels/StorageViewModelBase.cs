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
    class StorageViewModelBase : BindableBase, IInteractionRequestAware, IStorageViewModel
    {
        private IObjectWorkerNotification notification;
        private IWorker worker;

        private string name;

        public StorageViewModelBase(BudgetObject budgetObject)
        {
            BudgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));

            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        #region IInteractionRequestAware

        public INotification Notification
        {
            get { return notification; }
            set
            {
                notification = value as IObjectWorkerNotification;
                if (notification == null)
                {
                    throw new InvalidOperationException();
                }
                worker = notification.Worker;
                worker.Init(this);
            }
        }

        public Action FinishInteraction { get; set; }

        #endregion

        #region IStorageViewModel

        public BudgetObject BudgetObject { get; }

        public string Name
        {
            get { return name; }
            set
            {
                if (SetProperty(ref name, value))
                {
                    OnNameChanged(this, EventArgs.Empty);
                }
            }
        }

        #endregion

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        protected virtual void OnNameChanged(object sender, EventArgs e) { }

        private void OkExecute()
        {
            worker.Save(this);
            FinishInteraction();
            worker = null;
        }

        private void CancelExecute()
        {
            FinishInteraction();
            worker = null;
        }
    }
}