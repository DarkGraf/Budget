using Budget.Bll.DomainObjects;

namespace Budget.Bll
{
    public interface IBudgetDataProvider
    {
        FinanceStorage[] GetFinanceStorages();
        void AddFinanceStorage(string storageName);
        void UpdateFinanceStorage(long id, string storageName);
        void DeleteFinanceStorage(long id);

        FinanceArticle[] GetFinanceArticles();
        void AddFinanceArticle(string articleName);
        void UpdateFinanceArticle(long id, string articleName);
        void DeleteFinanceArticle(long id);
    }
}