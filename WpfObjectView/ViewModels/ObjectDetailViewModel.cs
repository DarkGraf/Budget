using System;

namespace WpfObjectView.ViewModels
{
    class ObjectDetailViewModel : ConfirmationViewModelBase
    {
        public ObjectDetailViewModel(object item, Action saveAction)
        {
            Item = item;
            SaveAction = saveAction;
        }

        public object Item { get; }

        public Action SaveAction { get; }

#warning Пока коммуникация идет через свойство, придумать более правильный способ.
        public bool UpdateFlag { get; set; }

        protected override void OkExecute()
        {
            UpdateFlag = true;
            RaisePropertyChanged(nameof(UpdateFlag));
            SaveAction();
            base.OkExecute();
        }
    }
}