using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using WpfObjectView.Notifications;

namespace WpfObjectView.ViewModels
{
    public abstract class ObjectListViewModel<T> : BindableBase
        where T : new()
    {
        T selectedItem;

        public ObjectListViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItemExecute);
            EditItemCommand = new DelegateCommand(EditItemExecute);
            DeleteItemCommand = new DelegateCommand(DeleteItemExecute);

            AddObjectNotificationRequest = new InteractionRequest<IObjectNotification>();
            EditObjectNotificationRequest = new InteractionRequest<IObjectNotification>();
            DeleteObjectNotificationRequest = new InteractionRequest<IConfirmation>();
        }

        public IEnumerable<T> Items
        {
            get { return GetItems(); }
        }

        public T SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        public InteractionRequest<IObjectNotification> AddObjectNotificationRequest { get; }
        public InteractionRequest<IObjectNotification> EditObjectNotificationRequest { get; }
        public InteractionRequest<IConfirmation> DeleteObjectNotificationRequest { get; }

        protected abstract IEnumerable<T> GetItems();

        private void AddItemExecute()
        {
            T item = new T();
            AddObjectNotificationRequest.Raise(new ObjectNotification("Добавление", item, () => AddSaveItem(item)));
        }

        private void EditItemExecute()
        {
            if (SelectedItem != null)
            {
                EditObjectNotificationRequest.Raise(new ObjectNotification("Редактирование", SelectedItem, () => EditSaveItem(SelectedItem)));
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
                        DeleteSaveItem(SelectedItem);
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


        protected virtual void AddSaveItem(T item) { }

        protected virtual void EditSaveItem(T item) { }

        protected virtual void DeleteSaveItem(T item) { }
    }
}