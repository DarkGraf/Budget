using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Budget.ViewModels
{
    public class MenuItemViewModel
    {
        public MenuItemViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemViewModel>();
        }

        /// <summary>
        /// Уникальное имя меню, для возможности добавления дочерних элементов.
        /// </summary>
        public string Id { get; set; }

        public string Header { get; set; }

        public ICommand Command { get; set; }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }
    }
}