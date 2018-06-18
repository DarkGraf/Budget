using Budget.Bll.DomainObjects;

namespace Budget.Bll
{
    public interface IBudgetDataProvider
    {
        FinanceStorage[] GetFinanceStorages();
        void AddFinanceStorage(FinanceStorage storage);
        void UpdateFinanceStorage(FinanceStorage storage);
        void DeleteFinanceStorage(long id);

        FinanceArticle[] GetFinanceArticles();
        void AddFinanceArticle(FinanceArticle article);
        void UpdateFinanceArticle(FinanceArticle article);
        void DeleteFinanceArticle(long id);
    }
}