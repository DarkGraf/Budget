using Budget.Bll.DomainObjects;

namespace Budget.Bll
{
    public interface IBudgetDataProvider
    {
        FinanceStorage[] GetFinanceStorages();
        void AddFinanceStorage(string storageName);
        void UpdateFinanceStorage(long id, string storageName);
        void DeleteFinanceStorage(long id);
    }
}