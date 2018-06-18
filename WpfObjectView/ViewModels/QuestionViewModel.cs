using Prism.Interactivity.InteractionRequest;

namespace WpfObjectView.ViewModels
{
    class QuestionViewModel : ConfirmationViewModelBase<IConfirmation>
    {
        private string message;

        public string Message
        {
            get { return message; }
            private set { SetProperty(ref message, value); }
        }

        protected override void OnConfirmationChanged()
        {
            Message = Confirmation.Content?.ToString();
        }
    }
}