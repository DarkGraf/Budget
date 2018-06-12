using Budget.ViewModels.Workers;
using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    interface IAddOrUpdateObjectNotification : INotification
    {
        IWorker Worker { get; }
    }
}