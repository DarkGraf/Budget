using WpfObjectView.Notifications;

namespace WpfObjectView.ViewModels
{
    class ObjectViewModel : ConfirmationViewModelBase<IObjectConfirmation>
    {
        private object item;

        public object Item
        {
            get { return item; }
            private set { SetProperty(ref item, value); }
        }

#warning Пока коммуникация идет через свойство, придумать более правильный способ.
        public bool UpdateFlag { get; set; }

        protected override void OnConfirmationChanged()
        {
            Item = Confirmation.Item;
        }

        protected override void OkExecute()
        {
            UpdateFlag = true;
            RaisePropertyChanged(nameof(UpdateFlag));
            Confirmation.SaveAction();
            base.OkExecute();
        }
    }
}