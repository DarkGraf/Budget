using Budget.Bll;
using Budget.Bll.DomainObjects;
using Budget.Bll.DomainObjects.Enums;
using System;
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
            new FinanceArticle { Id = 1, Name = "Зарплата", Type = FinanceArticleType.Income },
            new FinanceArticle { Id = 2, Name = "Покупки в магазине", Type = FinanceArticleType.Cost },
            new FinanceArticle { Id = 3, Name = "Прочие расходы", Type = FinanceArticleType.Cost },
            new FinanceArticle { Id = 4, Name = "Страховка", Type = FinanceArticleType.Cost },
            new FinanceArticle { Id = 5, Name = "Аванс", Type = FinanceArticleType.Income }
        };

        List<FinanceOperation> financeOperations;

        public BudgetMemoryDataProvider()
        {
            financeOperations = new List<FinanceOperation>
            {
                new FinanceOperation { Id = 1, Date = new DateTime(2018, 5, 20), Article = financeArticles.First(a => a.Id == 1), Sum = 50000 },
                new FinanceOperation { Id = 2, Date = new DateTime(2018, 5, 21), Article = financeArticles.First(a => a.Id == 2), Sum = 1500 },
                new FinanceOperation { Id = 3, Date = new DateTime(2018, 5, 21), Article = financeArticles.First(a => a.Id == 3), Sum = 5000 }
            };
        }

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
            // Обновлять не надо, так как UI уже обновил.
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
            // Обновлять не надо, так как UI уже обновил.
        }

        #endregion

        #region Operation

        public void AddFinanceOperation(FinanceOperation operation)
        {
            operation.Id = financeOperations.Select(s => s.Id).Max() + 1;
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