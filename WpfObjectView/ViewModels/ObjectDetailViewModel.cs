using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;
using WpfObjectView.Behaviors;

namespace WpfObjectView.ViewModels
{
    class ObjectDetailViewModel : BindableBase, IDialogHostViewModel
    {
        public ObjectDetailViewModel(object item, Action action)
        {
            Item = item;
            Action = action;

            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

#warning Сделать одну команду с параметром.
        protected virtual void OkExecute()
        {
            UpdateFlag = true;
            RaisePropertyChanged(nameof(UpdateFlag));
            Action();
            DialogHost.Close();
        }

        protected virtual void CancelExecute()
        {
            DialogHost.Close();
        }

        #region IDialogHostViewModel

        public IDialogHost DialogHost { get; set; }

        #endregion

        public object Item { get; }

        public Action Action { get; }

#warning Пока коммуникация идет через свойство, придумать более правильный способ.
        public bool UpdateFlag { get; set; }
    }
}