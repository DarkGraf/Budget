using Budget.ViewModels.Workers;
using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    class AddOrUpdateObjectNotification : Notification, IAddOrUpdateObjectNotification
    {
        public AddOrUpdateObjectNotification(string title, IWorker worker)
        {
            Title = title;
            Worker = worker;
        }

        public IWorker Worker { get; }
    }
}