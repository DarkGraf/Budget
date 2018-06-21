using Budget.Bll;
using Budget.Bll.DomainObjects;
using Budget.Dal.EF;
using Budget.Dal.Mappers;
using System;

namespace Budget.Dal
{
    public class BudgetSQLiteDataProvider : IBudgetDataProvider
    {
        readonly BudgetContext budgetContext;

        readonly CRUDGenericReposiroty<Entities.FinanceArticle, FinanceArticle> articleRepository;
        readonly CRUDGenericReposiroty<Entities.FinanceStorage, FinanceStorage> storageRepository;
        readonly CRUDGenericReposiroty<Entities.FinanceOperation, FinanceOperation> operationRepository;

        public BudgetSQLiteDataProvider(BudgetContext budgetContext, IMapper mapper)
        {
            this.budgetContext = budgetContext ?? throw new ArgumentNullException(nameof(budgetContext));
            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            articleRepository = new CRUDGenericReposiroty<Entities.FinanceArticle, FinanceArticle>(budgetContext, mapper);
            storageRepository = new CRUDGenericReposiroty<Entities.FinanceStorage, FinanceStorage>(budgetContext, mapper);
            operationRepository = new CRUDGenericReposiroty<Entities.FinanceOperation, FinanceOperation>(budgetContext, mapper);
        }

        #region Article

        public void AddFinanceArticle(FinanceArticle article)
        {
            articleRepository.Add(article);
            budgetContext.SaveChanges();
        }

        public void DeleteFinanceArticle(long id)
        {
            articleRepository.Delete(id);
            budgetContext.SaveChanges();
        }

        public FinanceArticle[] GetFinanceArticles()
        {
            return articleRepository.GetAll();
        }

        public void UpdateFinanceArticle(FinanceArticle article)
        {
            articleRepository.Update(article);
            budgetContext.SaveChanges();
        }

        #endregion

        #region Storage

        public void AddFinanceStorage(FinanceStorage storage)
        {
            storageRepository.Add(storage);
            budgetContext.SaveChanges();
        }

        public void DeleteFinanceStorage(long id)
        {
            storageRepository.Delete(id);
            budgetContext.SaveChanges();
        }

        public FinanceStorage[] GetFinanceStorages()
        {
            return storageRepository.GetAll();
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            storageRepository.Update(storage);
            budgetContext.SaveChanges();
        }

        #endregion

        #region Operation

        public void AddFinanceOperation(FinanceOperation operation)
        {
            operationRepository.Add(operation);
            budgetContext.SaveChanges();
        }

        public void DeleteFinanceOperation(long id)
        {
            operationRepository.Delete(id);
            budgetContext.SaveChanges();
        }

        public FinanceOperation[] GetFinanceOperations()
        {
            return operationRepository.GetAll(o => o.Article);
        }

        public void UpdateFinanceOperation(FinanceOperation operation)
        {
            operationRepository.Update(operation);
            budgetContext.SaveChanges();
        }

        #endregion
    }
}