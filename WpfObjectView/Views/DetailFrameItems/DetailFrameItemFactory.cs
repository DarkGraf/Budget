using System.Reflection;

namespace WpfObjectView.Views.DetailFrameItems
{
    class DetailFrameItemFactory
    {
        public DetailFrameItemBase Create(PropertyInfo info, object viewModel)
        {
            DetailFrameItemBase item;

            if (info.PropertyType.IsEnum)
            {
                item = new DetailFrameComboBoxItem(info);
            }
            else if (info.PropertyType.IsClass && info.PropertyType != typeof(string))
            {
                item = new DetailFrameListItem(info, viewModel);
            }
            else
            {
                item = new DetailFrameTextBoxItem(info);
            }

            return item;
        }
    }
}