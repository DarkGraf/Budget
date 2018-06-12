using Budget.Services.Interfaces;
using Budget.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Budget.Services
{
    public class MenuService : IMenuService
    {
        readonly IUnityContainer unityContainer;
        readonly IRegionManager regionManager;

        public MenuService(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.unityContainer = unityContainer;
            this.regionManager = regionManager;

            MenuItems = new ObservableCollection<MenuItemViewModel>();
        }

        public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        public void CreateParentMenu(string menuId, string header)
        {
            MenuItemViewModel item = new MenuItemViewModel();
            item.Id = menuId;
            item.Header = header;
            MenuItems.Add(item);
        }

        public void RegisterViewWithRegionForMenu<T>(string parentMenuId, string header, string regionName)
        {
            MenuItemViewModel parentMenu = GetMenuByName(parentMenuId);
            MenuItemViewModel item = new MenuItemViewModel();
            item.Header = header;
            item.Command = new DelegateCommand(() =>
            {
                regionManager.RequestNavigate(regionName, typeof(T).Name);
            });
            parentMenu.MenuItems.Add(item);

            unityContainer.RegisterTypeForNavigation<T>();
        }

        public void RegisterCommand(string parentMenuId, string header, ICommand command)
        {
            MenuItemViewModel parentMenu = GetMenuByName(parentMenuId);
            MenuItemViewModel item = new MenuItemViewModel();
            item.Header = header;
            item.Command = command;
            parentMenu.MenuItems.Add(item);
        }

        private MenuItemViewModel GetMenuByName(string menuId)
        {
            IEnumerable<MenuItemViewModel> items = MenuItems;
            MenuItemViewModel item = null;

            while (items.Count() > 0)
            {
                item = items.FirstOrDefault(v => v.Id.Equals(menuId, StringComparison.Ordinal));
                if (item != null)
                {
                    break;
                }
                else
                {
                    items = items.SelectMany(v => v.MenuItems);
                }
            }

            return item;
        }
    }
}