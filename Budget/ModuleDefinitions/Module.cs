using Budget.Bll;
using Budget.Bll.DomainObjects;
using Budget.Dal;
using Budget.Services;
using Budget.Services.Interfaces;
using Budget.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Budget.ModuleDefinitions
{
    class Module : IModule
    {
        readonly IRegionManager regionManager;
        readonly IUnityContainer unityContainer;

        public Module(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        public void Initialize()
        {
            unityContainer.RegisterType<IMenuService, MenuService>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<BudgetObject>(new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IBudgetDataProvider, BudgetMemoryDataProvider>();

            regionManager.RegisterViewWithRegion("MenuRegion", typeof(MenuView));

            IMenuService menuService = unityContainer.Resolve<IMenuService>();
            menuService.CreateParentMenu("Main", "Главное");
            menuService.CreateParentMenu("Dictionaries", "Справочники");
            menuService.RegisterViewWithRegionForMenu<OperationsView>("Main", "Операции", "MainRegion");
            menuService.RegisterViewWithRegionForMenu<StoragesView>("Dictionaries", "Финансовое хранение", "MainRegion");
            menuService.RegisterViewWithRegionForMenu<ArticlesView>("Dictionaries", "Статьи доходов-расходов", "MainRegion");
        }
    }
}