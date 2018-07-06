using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfObjectView.Views.DetailFrameItems
{
    class DetailFrameComboBoxItem : DetailFrameItemBase
    {
        readonly ComboBox comboBox;

        public DetailFrameComboBoxItem(PropertyInfo info) 
            : base(info)
        {
            comboBox = new ComboBox();
        }

        public override Control EditorControl
        {
            get { return comboBox; }
        }

        protected override DependencyProperty BoundDependencyProperty
        {
            get { return ComboBox.SelectedItemProperty; }
        }

        protected override void InternalInitialize()
        {
            comboBox.ItemsSource = Enum.GetValues(Info.PropertyType);
        }
    }
}