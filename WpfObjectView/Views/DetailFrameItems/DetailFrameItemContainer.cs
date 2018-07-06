using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace WpfObjectView.Views.DetailFrameItems
{
    class DetailFrameItemContainer
    {
        readonly List<BindingExpressionBase> bindingExpressions;
        readonly DetailFrameItemFactory itemFactory;

        public DetailFrameItemContainer()
        {
            bindingExpressions = new List<BindingExpressionBase>();
            itemFactory = new DetailFrameItemFactory();
        }

        public DetailFrameItemBase Create(PropertyInfo info, object viewModel)
        {
            Binding binding = new Binding();
            binding.Path = new PropertyPath(info.Name);
            binding.Mode = info.CanWrite ? BindingMode.TwoWay : BindingMode.OneWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;

            DetailFrameItemBase item = itemFactory.Create(info, viewModel);
            bindingExpressions.Add(item.SetBindingExpression(binding));
            return item;
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