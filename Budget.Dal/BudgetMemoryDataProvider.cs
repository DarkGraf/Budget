using Budget.Bll;
using Budget.Bll.DomainObjects;
using Budget.Bll.DomainObjects.Enums;
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
            new FinanceArticle { Id = 1, Name = "Aaa", Type = FinanceArticleType.Income },
            new FinanceArticle { Id = 2, Name = "Bbb", Type = FinanceArticleType.Cost },
            new FinanceArticle { Id = 3, Name = "Ccc", Type = FinanceArticleType.Income },
            new FinanceArticle { Id = 4, Name = "Ddd", Type = FinanceArticleType.Cost },
            new FinanceArticle { Id = 5, Name = "Eee", Type = FinanceArticleType.Income }
        };

        #region IBudgetDataProvider

        #region Storage

        public void AddFinanceStorage(FinanceStorage storage)
        {
            storage.Id = financeStorages.Select(s => s.Id).Max() + 1;
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
            FinanceStorage storageForUpdate = financeStorages.First(s => s.Id == storage.Id);
            if (storage != storageForUpdate)
            {
                storage.Name = storageForUpdate.Name;
            }
        }

        #endregion

        #region Article

        public void AddFinanceArticle(FinanceArticle article)
        {
            article.Id = financeArticles.Select(s => s.Id).Max() + 1;
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
            FinanceArticle articleForUpdate = financeArticles.First(s => s.Id == article.Id);
            if (article != articleForUpdate)
            {
                article.Name = articleForUpdate.Name;
                article.Type = articleForUpdate.Type;
            }
        }

        #endregion

        #endregion
    }
}