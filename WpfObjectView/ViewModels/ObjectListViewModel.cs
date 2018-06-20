using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using WpfObjectView.Notifications;
using WpfObjectView.ViewModels.Interfaces;

namespace WpfObjectView.ViewModels
{
    public abstract class ObjectListViewModel<T> : BindableBase, IObjectsEnumerable
        where T : new()
    {
        T selectedItem;

        public ObjectListViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItemExecute);
            EditItemCommand = new DelegateCommand(EditItemExecute);
            DeleteItemCommand = new DelegateCommand(DeleteItemExecute);

            AddObjectNotificationRequest = new InteractionRequest<IObjectConfirmation>();
            EditObjectNotificationRequest = new InteractionRequest<IObjectConfirmation>();
            DeleteObjectNotificationRequest = new InteractionRequest<IConfirmation>();
        }

        public IEnumerable<T> Items
        {
            get { return GetItems(); }
        }

        IEnumerable IObjectsEnumerable.Items { get => Items; }

        public T SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public InteractionRequest<IObjectConfirmation> AddObjectNotificationRequest { get; }
        public InteractionRequest<IObjectConfirmation> EditObjectNotificationRequest { get; }
        public InteractionRequest<IConfirmation> DeleteObjectNotificationRequest { get; }

        protected abstract IEnumerable<T> GetItems();

        private void AddItemExecute()
        {
            T item = new T();
            AddObjectNotificationRequest.Raise(new ObjectConfirmation("Добавление", item, () => AddItem(item)));
        }

        private void EditItemExecute()
        {
            if (SelectedItem != null)
            {
                EditObjectNotificationRequest.Raise(new ObjectConfirmation("Редактирование", SelectedItem, () => EditItem(SelectedItem)));
            }
        }

        private void DeleteItemExecute()
        {
            if (SelectedItem != null)
            {
                Action<IConfirmation> callback = r =>
                {
                    if (r.Confirmed)
                    {
                        DeleteItem(SelectedItem);
                    }
                };

                DeleteObjectNotificationRequest.Raise(new Confirmation
                {
                    Title = "Удаление",
                    Content = $"Удалить \"{SelectedItem.ToString()}\"?"
                },
                callback); 
            }
        }


        protected virtual void AddItem(T item) { }

        protected virtual void EditItem(T item) { }

        protected virtual void DeleteItem(T item) { }
    }
}