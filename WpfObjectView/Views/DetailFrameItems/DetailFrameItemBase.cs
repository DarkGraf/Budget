using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfObjectView.Views.DetailFrameItems
{
    abstract class DetailFrameItemBase
    {
        protected PropertyInfo Info { get; }

        public DetailFrameItemBase(PropertyInfo info)
        {
            Info = info;

            DisplayAttribute displayAttr = info.GetCustomAttribute<DisplayAttribute>();
            Label label = new Label();
            label.Content = displayAttr?.Name ?? info.Name;
            CaptionControl = label;
        }

        public Control CaptionControl { get; }

        abstract public Control EditorControl { get; }
        abstract protected DependencyProperty BoundDependencyProperty { get; }
        abstract protected void InternalInitialize();

        public BindingExpressionBase SetBindingExpression(Binding binding)
        {
            BindingExpressionBase bindingExpression = EditorControl.SetBinding(BoundDependencyProperty, binding);
            InternalInitialize();
            return bindingExpression;
        }
    }
}