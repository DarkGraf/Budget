using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    class DeleteObjectNotification<T> : Notification, IDeleteObjectNotification<T>
    {
        public DeleteObjectNotification(string title, T obj)
        {
            Title = title;
            Object = obj;
        }

        public T Object { get; }
    }
}