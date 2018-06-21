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
using System.Collections;

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
                    // Используя сервис локатор, запросим у Prism ObjectListViewModel<>, которая 
                    // реализует IObjectsEnumerable предоставляя доступ к коллекции объектов ObjectViewModelBase<>.
                    IObjectsEnumerable dataSource = ServiceLocator.Current.GetService(dataSourceType) as IObjectsEnumerable;
                    // Получим неизменяемую коллекцию элементов IRealKeyViewModel.
                    comboBox.ItemsSource = dataSource.Items.Cast<IRealKeyViewModel>().ToArray();

                    // Используя PropertyInfo получим значения свойства.
                    // Так как это произвольный класс, он должен быть наследником ObjectViewModelBase<>,
                    // который реализует IRealKeyViewModel. Используя данный интерфейс получим ключ доменного объекта.
                    long realKey = ((IRealKeyViewModel)info.GetValue(viewModel)).RealKey;

                    // По ключу доменного объекта найдем оборачивающую его модель представления.
                    IRealKeyViewModel obj = comboBox.ItemsSource.Cast<IRealKeyViewModel>().SingleOrDefault(v => v.RealKey == realKey);
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