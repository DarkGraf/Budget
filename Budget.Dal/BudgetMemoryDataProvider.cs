using Budget.Bll;
using Budget.Bll.DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace Budget.Dal
{
    public class BudgetMemoryDataProvider : IBudgetDataProvider
    {
        List<FinanceStorage> financeStorages = new List<FinanceStorage>
        {
            new FinanceStorage { Id = 1, Name = "Наличные"},
            new FinanceStorage { Id = 2, Name = "Банк 1"},
            new FinanceStorage { Id = 3, Name = "Банк 2"},
            new FinanceStorage { Id = 4, Name = "Банк 3"},
            new FinanceStorage { Id = 5, Name = "Банк 4"},
            new FinanceStorage { Id = 6, Name = "Банк 5"},
            new FinanceStorage { Id = 7, Name = "Банк 6"},
            new FinanceStorage { Id = 8, Name = "Банк 7"},
            new FinanceStorage { Id = 9, Name = "Банк 8"},
        };

        List<FinanceArticle> financeArticles = new List<FinanceArticle>
        {
            new FinanceArticle { Id = 1, Name = "Aaa" },
            new FinanceArticle { Id = 2, Name = "Bbb" },
            new FinanceArticle { Id = 3, Name = "Ccc" },
            new FinanceArticle { Id = 4, Name = "Ddd" },
            new FinanceArticle { Id = 5, Name = "Eee" }
        };

        #region IBudgetDataProvider

        public void AddFinanceStorage(string storageName)
        {
            FinanceStorage storage = new FinanceStorage
            {
                Id = financeStorages.Select(s => s.Id).Max() + 1,
                Name = storageName
            };
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

        public void UpdateFinanceStorage(long id, string storageName)
        {
            FinanceStorage storage = financeStorages.First(s => s.Id == id);
            storage.Name = storageName;
        }

        public void AddFinanceArticle(string articleName)
        {
            FinanceArticle article = new FinanceArticle
            {
                Id = financeArticles.Select(s => s.Id).Max() + 1,
                Name = articleName
            };
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

        public void UpdateFinanceArticle(long id, string articleName)
        {
            FinanceArticle article = financeArticles.First(s => s.Id == id);
            article.Name = articleName;
        }

        #endregion
    }
}