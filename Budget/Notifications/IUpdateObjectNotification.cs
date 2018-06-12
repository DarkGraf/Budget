using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    interface IUpdateObjectNotification<T> : INotification
    {
        T Object { get; }
    }
}