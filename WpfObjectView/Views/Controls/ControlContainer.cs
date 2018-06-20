using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfObjectView.Attributes;
using WpfObjectView.ViewModels.Interfaces;

namespace WpfObjectView.Views.Controls
{
    class ControlContainer
    {
        readonly List<BindingExpressionBase> bindingExpressions;

        public ControlContainer()
        {
            bindingExpressions = new List<BindingExpressionBase>();
        }

        public Control Create(PropertyInfo info, object viewModel)
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(info.Name);
            binding.Mode = info.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;

            Control control;
            if (info.PropertyType.IsEnum)
            {
                ComboBox comboBox = new ComboBox();
                comboBox.ItemsSource = Enum.GetValues(info.PropertyType);
                bindingExpressions.Add(comboBox.SetBinding(ComboBox.SelectedItemProperty, binding));
                control = comboBox;
            }
            else if (info.PropertyType.IsClass && info.PropertyType != typeof(string))
            {
                ComboBox comboBox = new ComboBox();
                DataSourceAttribute dataSourceAttr = info.GetCustomAttribute<DataSourceAttribute>();
                bindingExpressions.Add(comboBox.SetBinding(ComboBox.SelectedItemProperty, binding));
                Type dataSourceType = dataSourceAttr?.Type;
                if (dataSourceType != null)
                {
                    // Используя сервис локатор, запросим у Prism ObjectListViewModel<T>, которая 
                    // реализует IObjectsEnumerable предоставляя доступ к коллекции объектов ObjectViewModelBase<T>.
                    IObjectsEnumerable dataSource = ServiceLocator.Current.GetService(dataSourceType) as IObjectsEnumerable;
                    // Получим неизменяемую коллекцию из Linq.
                    comboBox.ItemsSource = dataSource.Items.Cast<IObject>().ToArray();

                    // Используя PropertyInfo получим значения свойства.
                    // Так как это произвольный класс, он должен быть наследником ObjectViewModelBase<T>,
                    // который реализует IObject. Используя данный интерфейс получим доменный объект.
                    object domainObject = ((IObject)info.GetValue(viewModel)).ObjectModel;

                    // По доменному объекту найдем оборачивающую его модель представления.
                    IObject obj = comboBox.ItemsSource.Cast<IObject>().SingleOrDefault(v => v.ObjectModel == domainObject);
                    // Если это не создание нового.
                    if (obj != null)
                    {
                        comboBox.SelectedItem = obj;
                    }
                }

                control = comboBox;
            }
            else
            {
                TextBox textBox = new TextBox();
                textBox.IsReadOnly = !info.CanWrite;
                bindingExpressions.Add(textBox.SetBinding(TextBox.TextProperty, binding));
                control = textBox;
            }

            return control;
        }

        public void Update()
        {
            foreach (var be in bindingExpressions)
            {
                be.UpdateSource();
            }
        }

        public void Clear()
        {
            bindingExpressions.Clear();
        }
    }
}