using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using WpfObjectView.Notifications;

namespace WpfObjectView.ViewModels
{
    class AddOrEditObjectViewModel : BindableBase, IInteractionRequestAware
    {
        private IObjectNotification notification;
        private object item;

        public AddOrEditObjectViewModel()
        {
            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        #region IInteractionRequestAware

        public INotification Notification
        {
            get { return notification; }
            set
            {
                notification = value as IObjectNotification;
                if (notification == null)
                {
                    throw new InvalidOperationException();
                }
                Item = notification.Item;
            }
        }

        public Action FinishInteraction { get; set; }

        #endregion

        public object Item
        {
            get { return item; }
            private set { SetProperty(ref item, value); }
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

#warning Пока коммуникация идет через свойство, придумать более правильный способ.
        public bool UpdateFlag { get; set; }

        private void OkExecute()
        {
            UpdateFlag = true;
            RaisePropertyChanged(nameof(UpdateFlag));
            notification.SaveAction();       
            FinishInteraction();
        }

        private void CancelExecute()
        {
            FinishInteraction();
        }
    }
}