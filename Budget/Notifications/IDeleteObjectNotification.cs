using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    interface IDeleteObjectNotification<T> : INotification
    {
        T Object { get; }
    }
}