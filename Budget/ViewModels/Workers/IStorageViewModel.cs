using Budget.Bll.DomainObjects;

namespace Budget.ViewModels.Workers
{
    interface IStorageViewModel
    {
        BudgetObject BudgetObject { get; }
        string Name { get; set; }
    }
}