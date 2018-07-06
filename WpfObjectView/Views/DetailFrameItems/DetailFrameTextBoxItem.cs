using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfObjectView.Views.DetailFrameItems
{
    class DetailFrameTextBoxItem : DetailFrameItemBase
    {
        readonly TextBox textBox;

        public DetailFrameTextBoxItem(PropertyInfo info) 
            : base(info)
        {
            textBox = new TextBox();
        }

        public override Control EditorControl
        {
            get { return textBox; }
        }

        protected override DependencyProperty BoundDependencyProperty
        {
            get { return TextBox.TextProperty; }
        }

        protected override void InternalInitialize()
        {
            textBox.IsReadOnly = !Info.CanWrite;
        }
    }
}