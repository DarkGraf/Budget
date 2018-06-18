using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using WpfObjectView.Attributes;

namespace WpfObjectView.Behaviors
{
    class DataGridSmartColumnsBehavior : Behavior<DataGrid>
    {
        private bool isInit = false;

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register(
                "IsEnabled",
                typeof(bool),
                typeof(DataGridSmartColumnsBehavior),
                new PropertyMetadata(false, IsEnabledPropertyChanged));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        private static void IsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            if (source is DataGridSmartColumnsBehavior behavior && behavior.IsEnabled != (bool)e.NewValue)
            {
                behavior.OnIsEnabledPropertyChanged();
            }
        }

        #endregion

        #region Behavior

        protected override void OnAttached()
        {
            base.OnAttached();
            // Для обработки ситуации, когда свойство IsEnabled установлено быстрее чем ItemsSource,
            // будем отслеживать также изменение свойства ItemsSource.
            var descriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (descriptor != null)
            {
                descriptor.AddValueChanged(AssociatedObject, OnItemsSourceChanged);
            }
            OnIsEnabledPropertyChanged();
        }

        protected override void OnDetaching()
        {
            var descriptor = DependencyPropertyDescriptor.FromProperty(ItemsControl.ItemsSourceProperty, typeof(DataGrid));
            if (descriptor != null)
            {
                descriptor.RemoveValueChanged(AssociatedObject, OnItemsSourceChanged);
            }
            base.OnDetaching();
        }

        #endregion

        private void OnItemsSourceChanged(object sender, EventArgs e)
        {
            OnIsEnabledPropertyChanged();
        }

        private void OnIsEnabledPropertyChanged()
        {
            if (IsEnabled && AssociatedObject.ItemsSource != null && !isInit)
            {
                // Получим тип элементов коллекции.
                Type type = GetElementTypeOfEnumerable(AssociatedObject.ItemsSource);

                if (type != null)
                {
                    FillColumns(type);
                    isInit = true;
                }
            }
        }

        private void FillColumns(Type type)
        {
            PropertyInfo[] infos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var info in infos)
            {
                if (!(info.GetCustomAttribute<VisibleInViewAttribute>()?.VisibleInList ?? true))
                {
                    continue;
                }

                var displayAttr = info.GetCustomAttribute<DisplayAttribute>();

                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = displayAttr?.Name ?? info.Name;
                column.Binding = new Binding(info.Name);
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                AssociatedObject.Columns.Add(column);
            }
        }

        private static Type GetElementTypeOfEnumerable(object o)
        {
            if (!(o is IEnumerable enumerable))
            {
                return null;
            }

            Type elementType = (from i in enumerable.GetType().GetInterfaces()
                                where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                select i.GetGenericArguments()[0]).FirstOrDefault();

            if (elementType == null || elementType == typeof(object))
            {
                object firstElement = enumerable.Cast<object>().FirstOrDefault();
                if (firstElement != null)
                {
                    elementType = firstElement.GetType();
                }
            }
            return elementType;
        }
    }
}