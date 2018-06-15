using System;
using Prism.Interactivity.InteractionRequest;

namespace WpfObjectView.Notifications
{
    class ObjectNotification : Notification, IObjectNotification
    {
        public ObjectNotification(string title, object item, Action saveAction)
        {
            Title = title;
            Item = item;
            SaveAction = saveAction;
        }

        public object Item { get; }

        public Action SaveAction { get; }
    }
}