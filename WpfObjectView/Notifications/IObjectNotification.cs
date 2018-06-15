using Prism.Interactivity.InteractionRequest;
using System;

namespace WpfObjectView.Notifications
{
    public interface IObjectNotification : INotification
    {
        object Item { get; }
        Action SaveAction { get; }
    }
}