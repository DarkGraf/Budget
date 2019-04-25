using Prism.Commands;
using Prism.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using WpfObjectView.Behaviors;
using WpfObjectView.ViewModels.Interfaces;

namespace WpfObjectView.ViewModels
{
    public abstract class ObjectListViewModel<T> : BindableBase, IObjectsEnumerable, IDialogHostViewModel
        where T : new()
    {
        T selectedItem;

        public ObjectListViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItemExecute);
            EditItemCommand = new DelegateCommand(EditItemExecute);
            DeleteItemCommand = new DelegateCommand(DeleteItemExecute);
            RefreshCommand = new DelegateCommand(RefreshExecute);
        }

        public IEnumerable<T> Items
        {
            get { return GetItems(); }
        }

        IEnumerable IObjectsEnumerable.Items { get => Items; }

        #region IDialogHostViewModel

        public IDialogHost DialogHost { get; set; }

        #endregion

        public T SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand RefreshCommand { get; }

        protected abstract IEnumerable<T> GetItems();

        private void AddItemExecute()
        {
            T item = new T();
            ObjectDetailViewModel viewModel = new ObjectDetailViewModel(item, () => AddItem(item));
            DialogHost.Show(viewModel, "Добавление");
        }

        private void EditItemExecute()
        {
            if (SelectedItem != null)
            {
                ObjectDetailViewModel viewModel = new ObjectDetailViewModel(SelectedItem, () => EditItem(SelectedItem));
                DialogHost.Show(viewModel, "Редактирование");
            }
        }

        private void DeleteItemExecute()
        {
            if (SelectedItem != null)
            {
                if (DialogHost.ShowQuestion($@"Удалить ""{SelectedItem.ToString()}""?") == true)
                {
                    DeleteItem(SelectedItem);
                }
            }
        }

        protected virtual void AddItem(T item) { }

        protected virtual void EditItem(T item) { }

        protected virtual void DeleteItem(T item) { }

        private void RefreshExecute()
        {
            RaisePropertyChanged(nameof(Items));
        }
    }
}