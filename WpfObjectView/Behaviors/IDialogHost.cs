namespace WpfObjectView.Behaviors
{
    public interface IDialogHost
    {
        bool? Show(object dataContext, string caption = null);
        bool? ShowQuestion(string message, bool showCancel = false);
        void Close();
    }
}