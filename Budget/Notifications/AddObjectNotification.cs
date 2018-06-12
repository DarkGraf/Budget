using Prism.Interactivity.InteractionRequest;

namespace Budget.Notifications
{
    class AddObjectNotification : Notification, IAddObjectNotification
    {
        public AddObjectNotification(string title)
        {
            Title = title;
        }
    }
}