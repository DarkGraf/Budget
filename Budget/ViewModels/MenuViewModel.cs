using Budget.Services.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace Budget.ViewModels
{
    class MenuViewModel
    {
        readonly IMenuService menuService;

        public MenuViewModel(IMenuService menuService)
        {
            if (menuService == null)
            {
                throw new ArgumentNullException(nameof(menuService));
            }

            this.menuService = menuService;
        }

        public ObservableCollection<MenuItemViewModel> MenuItems
        {
            get { return menuService.MenuItems; }
        }
    }
}