using Prism.Interactivity.InteractionRequest;
using System;

namespace WpfObjectView.Notifications
{
    public interface IObjectConfirmation : IConfirmation
    {
        object Item { get; }
        Action SaveAction { get; }
    }
}