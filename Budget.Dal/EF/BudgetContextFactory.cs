using Budget.Bll.Interfaces;

namespace Budget.Dal.EF
{
    // Фабрика нужна для создания объекта без ссылок сборки не EF.
    public static class BudgetContextFactory
    {
         public static IBudgetUnitOfWork Create(string nameOrConnectionString)
        {
            return new BudgetContext(nameOrConnectionString);
        }

    }
}