using Budget.Bll;
using Budget.Bll.DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Dal
{
    public class BudgetMemoryDataProvider : IBudgetDataProvider
    {
        List<FinanceStorage> financeStorages = new List<FinanceStorage>();
        List<FinanceArticle> financeArticles = new List<FinanceArticle>();
        List<FinanceOperation> financeOperations = new List<FinanceOperation>();

        #region IBudgetDataProvider

        #region Storage

        public void AddFinanceStorage(FinanceStorage storage)
        {
            storage.Id = financeStorages.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1;
            financeStorages.Add(storage);
        }

        public void DeleteFinanceStorage(long id)
        {
            FinanceStorage storage = financeStorages.First(s => s.Id == id);
            financeStorages.Remove(storage);
        }

        public FinanceStorage[] GetFinanceStorages()
        {
            return financeStorages.ToArray();
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            // Обновлять не надо, так как UI уже обновил.
        }

        #endregion

        #region Article

        public void AddFinanceArticle(FinanceArticle article)
        {
            article.Id = financeArticles.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1;
            financeArticles.Add(article);
        }

        public void DeleteFinanceArticle(long id)
        {
            FinanceArticle article = financeArticles.First(s => s.Id == id);
            financeArticles.Remove(article);
        }

        public FinanceArticle[] GetFinanceArticles()
        {
            return financeArticles.ToArray();
        }

        public void UpdateFinanceArticle(FinanceArticle article)
        {
            // Обновлять не надо, так как UI уже обновил.
        }

        #endregion

        #region Operation

        public void AddFinanceOperation(FinanceOperation operation)
        {
            operation.Id = financeOperations.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1;
            financeOperations.Add(operation);
        }

        public void DeleteFinanceOperation(long id)
        {
            FinanceOperation operation = financeOperations.First(s => s.Id == id);
            financeOperations.Remove(operation);
        }

        public FinanceOperation[] GetFinanceOperations()
        {
            return financeOperations.ToArray();
        }

        public void UpdateFinanceOperation(FinanceOperation operation)
        {
            // Обновлять не надо, так как UI уже обновил.
        }

        #endregion

        #endregion
    }
}