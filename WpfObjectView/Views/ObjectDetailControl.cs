using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfObjectView.Attributes;
using WpfObjectView.Views.Controls;

namespace WpfObjectView.Views
{
    public class ObjectDetailControl : ContentControl
    {
        #region UpdateFlagProperty

        public static readonly DependencyProperty UpdateFlagProperty = DependencyProperty.Register(
            "UpdateFlag",
            typeof(bool),
            typeof(ObjectDetailControl),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, UpdateFlagPropertyChanged));

        public bool UpdateFlag
        {
            get { return (bool)GetValue(UpdateFlagProperty); }
            set { SetValue(UpdateFlagProperty, value); }
        }

        private static void UpdateFlagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ObjectDetailControl view && view.UpdateFlag)
            {
                view.Update();
                view.UpdateFlag = false;
            }
        }

        #endregion

        readonly Grid mainGrid;
        readonly ControlContainer controlContainer;
        
        public ObjectDetailControl()
        {
            Content = mainGrid = new Grid();
            controlContainer = new ControlContainer();

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
            controlContainer.Update();
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
            controlContainer.Clear();
            mainGrid.Children.Clear();
            mainGrid.RowDefinitions.Clear();
        }

        private void LoadProperties()
        {
            Type type = DataContext.GetType();
            PropertyInfo[] infos = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (PropertyInfo info in infos)
            {
                if (!(info.GetCustomAttribute<VisibleInViewAttribute>()?.VisibleInList ?? true))
                {
                    continue;
                }

                mainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });

                var displayAttr = info.GetCustomAttribute<DisplayAttribute>();

                Label label = new Label();
                label.Content = displayAttr?.Name ?? info.Name;

                Grid.SetRow(label, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(label);

                Control control = controlContainer.Create(info, DataContext);
                control.Margin = new Thickness(5);
                Grid.SetColumn(control, 1);
                Grid.SetRow(control, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(control);
            }
        }
    }
}