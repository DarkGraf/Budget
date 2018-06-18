using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace WpfObjectView.ViewModels
{
    abstract class ConfirmationViewModelBase<T> : BindableBase, IInteractionRequestAware
        where T : IConfirmation
    {
        public ConfirmationViewModelBase()
        {
            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        #region IInteractionRequestAware

        public INotification Notification
        {
            get { return Confirmation; }
            set
            {
                Confirmation = (T)value;
                OnConfirmationChanged();
            }
        }

        public Action FinishInteraction { get; set; }

        #endregion

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        protected T Confirmation { get; private set; }

        protected virtual void OnConfirmationChanged() { }

        protected virtual void OkExecute()
        {
            Confirmation.Confirmed = true;
            FinishInteraction();
        }

        protected virtual void CancelExecute()
        {
            FinishInteraction();
        }
    }
}