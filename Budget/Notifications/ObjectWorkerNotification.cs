using Budget.ViewModels.Workers;
using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    class ObjectWorkerNotification : Notification, IObjectWorkerNotification
    {
        public ObjectWorkerNotification(string title, IWorker worker)
        {
            Title = title;
            Worker = worker;
        }

        public IWorker Worker { get; }
    }
}