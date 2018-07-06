using Microsoft.Practices.ServiceLocation;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WpfObjectView.Attributes;
using WpfObjectView.ViewModels.Interfaces;

namespace WpfObjectView.Views.DetailFrameItems
{
    class DetailFrameListItem : DetailFrameItemBase
    {
        readonly object viewModel;
        readonly ComboBox comboBox;

        public DetailFrameListItem(PropertyInfo info, object viewModel)
            : base(info)
        {
            this.viewModel = viewModel;
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
            DataSourceAttribute dataSourceAttr = Info.GetCustomAttribute<DataSourceAttribute>();
            Type dataSourceType = dataSourceAttr?.Type;
            if (dataSourceType != null)
            {
                // Используя сервис локатор, запросим у Prism ObjectListViewModel<>, которая 
                // реализует IObjectsEnumerable предоставляя доступ к коллекции объектов ObjectViewModelBase<>.
                IObjectsEnumerable dataSource = ServiceLocator.Current.GetService(dataSourceType) as IObjectsEnumerable;
                // Получим неизменяемую коллекцию элементов IRealKeyViewModel.
                comboBox.ItemsSource = dataSource.Items.Cast<IRealKeyViewModel>().ToArray();

                // Используя PropertyInfo получим значения свойства.
                // Так как это произвольный класс, он должен быть наследником ObjectViewModelBase<>,
                // который реализует IRealKeyViewModel. Используя данный интерфейс получим ключ доменного объекта.
                long realKey = ((IRealKeyViewModel)Info.GetValue(viewModel)).RealKey;

                // По ключу доменного объекта найдем оборачивающую его модель представления.
                IRealKeyViewModel obj = comboBox.ItemsSource.Cast<IRealKeyViewModel>().SingleOrDefault(v => v.RealKey == realKey);
                // Если это не создание нового.
                if (obj != null)
                {
                    comboBox.SelectedItem = obj;
                }
            }
        }
    }
}