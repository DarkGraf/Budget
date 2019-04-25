using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;
using WpfObjectView.Behaviors;

namespace WpfObjectView.ViewModels
{
    abstract class ConfirmationViewModelBase : BindableBase, IDialogHostViewModel
    {
        public ConfirmationViewModelBase()
        {
            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

#warning Сделать одну команду с параметром.
        protected virtual void OkExecute()
        {
            DialogHost.Close();
        }

        protected virtual void CancelExecute()
        {
            DialogHost.Close();
        }

        #region IDialogHostViewModel

        public IDialogHost DialogHost { get; set; }

        #endregion
    }
}