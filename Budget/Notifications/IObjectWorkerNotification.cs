using Budget.ViewModels.Workers;
using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    interface IObjectWorkerNotification : INotification
    {
        IWorker Worker { get; }
    }
}