using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace WpfObjectView.Behaviors
{
    public class DialogHostBehavior : Behavior<ContentControl>, IDialogHost
    {
        public static readonly DependencyProperty DialogControlTemplateProperty = DependencyProperty.Register(
            nameof(DialogControlTemplate), typeof(ControlTemplate), typeof(DialogHostBehavior), new PropertyMetadata(default(DataTemplate)));

        public ControlTemplate DialogControlTemplate
        {
            get { return (ControlTemplate)GetValue(DialogControlTemplateProperty); }
            set { SetValue(DialogControlTemplateProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            
            if (AssociatedObject != null)
            {
                // Возможна ситуация, когда DataContext еще не привязан.
                // Это обработает обработчик события на изменения DataContext.
                if (AssociatedObject.DataContext != null)
                {
                    if (AssociatedObject.DataContext is IDialogHostViewModel)
                    {
                        (AssociatedObject.DataContext as IDialogHostViewModel).DialogHost = this;
                    }
                }

                AssociatedObject.DataContextChanged += AssociatedObjectDataContextChanged;
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.DataContextChanged -= AssociatedObjectDataContextChanged;

                if (AssociatedObject.DataContext is IDialogHostViewModel && (AssociatedObject.DataContext as IDialogHostViewModel).DialogHost == this)
                {
                    (AssociatedObject.DataContext as IDialogHostViewModel).DialogHost = null;
                }
            }

            base.OnDetaching();
        }

        private void AssociatedObjectDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                if (e.OldValue is IDialogHostViewModel && (e.OldValue as IDialogHostViewModel).DialogHost == this)
                {
                    (e.OldValue as IDialogHostViewModel).DialogHost = null;
                }
            }

            if (e.NewValue != null)
            {
                if (e.NewValue is IDialogHostViewModel)
                {
                    (e.NewValue as IDialogHostViewModel).DialogHost = this;
                }
            }
        }

        #region IDialogHost

        public bool? Show(object dataContext, string caption)
        {
            ContentControl contentControl = new ContentControl();
            contentControl.Template = DialogControlTemplate;

            Window window = new Window();
            window.ResizeMode = ResizeMode.NoResize;
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.DataContext = dataContext;
            window.Content = contentControl;
            window.Title = caption;

            DialogHostBehavior behavior = new DialogHostBehavior();
            behavior.DialogControlTemplate = DialogControlTemplate;
            BehaviorCollection behaviors = Interaction.GetBehaviors(window);
            behaviors.Add(behavior);

            return window.ShowDialog();
        }

        public bool? ShowQuestion(string message, bool showCancel)
        {
            MessageBoxButton button = showCancel ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;

            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, message, Application.Current.MainWindow.Title,
                button, MessageBoxImage.Question);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return null;
            }
        }

        public void Close()
        {
            Window parentWindow = Window.GetWindow(AssociatedObject);
            parentWindow.Close();
        }

        #endregion
    }
}