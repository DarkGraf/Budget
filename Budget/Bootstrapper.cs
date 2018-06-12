using Budget.ModuleDefinitions;
using Budget.Views;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace Budget
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            Shell shell = new Shell();
            shell.Show();
            return shell;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            catalog.AddModule(typeof(Module));
            return catalog;
        }
    }
}