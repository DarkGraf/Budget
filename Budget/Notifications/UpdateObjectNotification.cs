using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    class UpdateObjectNotification<T> : Notification, IUpdateObjectNotification<T>
    {
        public UpdateObjectNotification(string title, T obj)
        {
            Title = title;
            Object = obj;
        }

        public T Object { get; }
    }
}