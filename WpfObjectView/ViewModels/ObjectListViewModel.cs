using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using System.Collections.Generic;
using System.Windows.Input;
using WpfObjectView.Notifications;

namespace WpfObjectView.ViewModels
{
    public abstract class ObjectListViewModel<T>
    {
        public ObjectListViewModel()
        {
            AddItemCommand = new DelegateCommand(AddItemExecute);
            EditItemCommand = new DelegateCommand(EditItemExecute);
            DeleteItemCommand = new DelegateCommand(DeleteItemExecute);

            AddObjectNotificationRequest = new InteractionRequest<IAddObjectNotification>();
            EditObjectNotificationRequest = new InteractionRequest<IEditObjectNotification>();
            DeleteObjectNotificationRequest = new InteractionRequest<IDeleteObjectNotification>();
        }

        public IEnumerable<T> Items
        {
            get { return GetItems(); }
        }

        public T SelectedItem { get; set; }

        public ICommand AddItemCommand { get; }
        public ICommand EditItemCommand { get; }
        public ICommand DeleteItemCommand { get; }

        internal InteractionRequest<IAddObjectNotification> AddObjectNotificationRequest { get; }
        internal InteractionRequest<IEditObjectNotification> EditObjectNotificationRequest { get; }
        internal InteractionRequest<IDeleteObjectNotification> DeleteObjectNotificationRequest { get; }

        protected abstract IEnumerable<T> GetItems();

        private void AddItemExecute()
        {
            AddObjectNotificationRequest.Raise(new AddObjectNotification { Title = "Добавление" });
        }

        private void EditItemExecute()
        {
            if (SelectedItem != null)
            {
                EditObjectNotificationRequest.Raise(new EditObjectNotification { Title = "Редактирование" });
            }
        }

        private void DeleteItemExecute()
        {
            if (SelectedItem != null)
            {
                DeleteObjectNotificationRequest.Raise(new DeleteObjectNotification { Title = "Удаление" });
            }
        }
    }
}