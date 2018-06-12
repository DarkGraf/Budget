using Budget.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Budget.Services.Interfaces
{
    public interface IMenuService
    {
        ObservableCollection<MenuItemViewModel> MenuItems { get; }

        /// <summary>
        /// Регистрация представления для навигации.
        /// </summary>
        /// <typeparam name="T">Тип представления.</typeparam>
        /// <param name="parentMenuId">Идентификатор меню-родителя.</param>
        /// <param name="header">Заголовок меню.</param>
        /// <param name="regionName">Имя региона.</param>
        void RegisterViewWithRegionForMenu<T>(string parentMenuId, string header, string regionName);

        /// <summary>
        /// Регистрация команды в меню.
        /// </summary>
        /// <param name="parentMenuId">Идентификатор меню-родителя.</param>
        /// <param name="header">Заголовок меню.</param>
        /// <param name="command">Команда.</param>
        void RegisterCommand(string parentMenuId, string header, ICommand command);

        /// <summary>
        /// Создания меню, содержащее другие элементы.
        /// </summary>
        /// <param name="menuId">Уникальный идентификатор меню.</param>
        /// <param name="header">Заголовок меню.</param>
        void CreateParentMenu(string menuId, string header);
    }
}