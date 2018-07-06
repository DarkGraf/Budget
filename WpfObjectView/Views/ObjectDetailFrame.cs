using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfObjectView.Attributes;
using WpfObjectView.Views.DetailFrameItems;

namespace WpfObjectView.Views
{
    public class ObjectDetailFrame : ContentControl
    {
        #region UpdateFlagProperty

        public static readonly DependencyProperty UpdateFlagProperty = DependencyProperty.Register(
            "UpdateFlag",
            typeof(bool),
            typeof(ObjectDetailFrame),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, UpdateFlagPropertyChanged));

        public bool UpdateFlag
        {
            get { return (bool)GetValue(UpdateFlagProperty); }
            set { SetValue(UpdateFlagProperty, value); }
        }

        private static void UpdateFlagPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ObjectDetailFrame view && view.UpdateFlag)
            {
                view.Update();
                view.UpdateFlag = false;
            }
        }

        #endregion

        readonly Grid mainGrid;
        readonly DetailFrameItemContainer itemContainer;
        
        public ObjectDetailFrame()
        {
            Content = mainGrid = new Grid();
            itemContainer = new DetailFrameItemContainer();

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
            itemContainer.Update();
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
            itemContainer.Clear();
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

                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                DetailFrameItemBase item = itemContainer.Create(info, DataContext);
                Control captionControl = item.CaptionControl;
                Control editorControl = item.EditorControl;

                Grid.SetRow(captionControl, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(captionControl);
                
                editorControl.Margin = new Thickness(5);
                Grid.SetColumn(editorControl, 1);
                Grid.SetRow(editorControl, mainGrid.RowDefinitions.Count - 1);
                mainGrid.Children.Add(editorControl);
            }
        }
    }
}