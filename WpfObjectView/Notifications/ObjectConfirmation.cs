using System;
using Prism.Interactivity.InteractionRequest;

namespace WpfObjectView.Notifications
{
    class ObjectConfirmation : Notification, IObjectConfirmation
    {
        public ObjectConfirmation(string title, object item, Action saveAction)
        {
            Title = title;
            Item = item;
            SaveAction = saveAction;
        }

        public object Item { get; }

        public Action SaveAction { get; }

        public bool Confirmed { get; set; }
    }
}