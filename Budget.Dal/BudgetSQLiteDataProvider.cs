using Budget.Bll;
using Budget.Bll.DomainObjects;
using Budget.Dal.EF;
using System;
using System.Linq;

namespace Budget.Dal
{
    public class BudgetSQLiteDataProvider : IBudgetDataProvider
    {
        readonly BudgetContext budgetContext;
        readonly IMapper mapper;

        public BudgetSQLiteDataProvider(BudgetContext budgetContext, IMapper mapper)
        {
            this.budgetContext = budgetContext ?? throw new ArgumentNullException(nameof(budgetContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #region Article

        public void AddFinanceArticle(FinanceArticle article)
        {
            var dalArticle = mapper.ArticleBllToDal(article);
            budgetContext.FinanceArticles.Add(dalArticle);
            budgetContext.SaveChanges();
        }

        public void DeleteFinanceArticle(long id)
        {
            var dalArticle = budgetContext.FinanceArticles.Find(id);
            budgetContext.FinanceArticles.Remove(dalArticle);
            budgetContext.SaveChanges();
        }

        public FinanceArticle[] GetFinanceArticles()
        {
            var dalArticles = budgetContext.FinanceArticles.ToArray();
            return mapper.ArticlesDalToBll(dalArticles).ToArray();
        }

        public void UpdateFinanceArticle(FinanceArticle article)
        {
            var dalArticle = budgetContext.FinanceArticles.Find(article.Id);
            if (dalArticle != null)
            {
                mapper.ArticleBllToDal(article, dalArticle);
                budgetContext.SaveChanges();
            }
        }

        #endregion

        #region Storage

        public void AddFinanceStorage(FinanceStorage storage)
        {
            var dalStorage = mapper.StorageBllToDal(storage);
            budgetContext.FinanceStorages.Add(dalStorage);
            budgetContext.SaveChanges();
        }

        public void DeleteFinanceStorage(long id)
        {
            var dalStorage = budgetContext.FinanceStorages.Find(id);
            budgetContext.FinanceStorages.Remove(dalStorage);
            budgetContext.SaveChanges();
        }

        public FinanceStorage[] GetFinanceStorages()
        {
            var dalStorages = budgetContext.FinanceStorages.ToArray();
            return mapper.StoragesDalToBll(dalStorages).ToArray();
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            var dalStorage = budgetContext.FinanceStorages.Find(storage.Id);
            if (dalStorage != null)
            {
                mapper.StorageBllToDal(storage, dalStorage);
                budgetContext.SaveChanges();
            }
        }

        #endregion

        #region Operation

        public void AddFinanceOperation(FinanceOperation operation)
        {
            throw new NotImplementedException();
        }

        public void DeleteFinanceOperation(long id)
        {
            throw new NotImplementedException();
        }

        public FinanceOperation[] GetFinanceOperations()
        {
            throw new NotImplementedException();
        }

        public void UpdateFinanceOperation(FinanceOperation operation)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}