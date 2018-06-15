using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfObjectView.Attributes;

namespace WpfObjectView.Views
{
    public class ObjectDetailView : ContentControl
    {
        #region UpdateFlagProperty

        public static readonly DependencyProperty UpdateFlagProperty = DependencyProperty.Register(
            "UpdateFlag",
            typeof(bool),
            typeof(ObjectDetailView),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, UpdateFlagPropertyChanged));

        public bool UpdateFlag
        {
            get { return (bool)GetValue(UpdateFlagProperty); }
            set { SetValue(UpdateFlagProperty, value); }
        }

        private static void UpdateFlagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ObjectDetailView view && view.UpdateFlag)
            {
                view.Update();
                view.UpdateFlag = false;
            }
        }

        #endregion

        readonly Grid mainGrid;
        readonly List<BindingExpressionBase> bindingExpressions;

        public ObjectDetailView()
        {
            Content = mainGrid = new Grid();
            bindingExpressions = new List<BindingExpressionBase>();

            mainGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = GridLength.Auto
            });
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public void Update()
        {
            foreach (var be in bindingExpressions)
            {
                be.UpdateSource();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                LoadProperties();
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            bindingExpressions.Clear();
            mainGrid.Children.Clear();
            mainGrid.RowDefinitions.Clear();
        }

        private void LoadProperties()
        {
            Type type = DataContext.GetType();
            PropertyInfo[] infos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo info in infos)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                var attr = info.GetCustomAttribute<SmartPropertyAttributeAttribute>();

                Label label = new Label();
                label.Content = attr?.Header ?? info.Name;

                Grid.SetRow(label, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(label);

                Binding binding = new Binding();
                binding.Path = new PropertyPath(info.Name);
                binding.Mode = info.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay;
                binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;

                TextBox textBox = new TextBox();
                textBox.Margin = new Thickness(5);
                textBox.IsReadOnly = !info.CanWrite;
                bindingExpressions.Add(textBox.SetBinding(TextBox.TextProperty, binding));

                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(textBox);
            }
        }
    }
}