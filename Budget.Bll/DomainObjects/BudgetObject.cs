using System;

namespace Budget.Bll.DomainObjects
{
    public sealed class BudgetObject
    {
        readonly IBudgetDataProvider dataProvider;

        public BudgetObject(IBudgetDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        #region Storage

        public FinanceStorage[] GetFinanceStorage()
        {
            return dataProvider.GetFinanceStorages();
        }

        public void AddFinanceStorage(FinanceStorage storage)
        {
            dataProvider.AddFinanceStorage(storage);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            dataProvider.UpdateFinanceStorage(storage);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceStorage(FinanceStorage storage)
        {
            dataProvider.DeleteFinanceStorage(storage.Id);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceStoragesChanged;

        #endregion

        #region Article

        public FinanceArticle[] GetFinanceArticle()
        {
            return dataProvider.GetFinanceArticles();
        }

        public void AddFinanceArticle(FinanceArticle article)
        {
            dataProvider.AddFinanceArticle(article);
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceArticle(FinanceArticle article)
        {
            dataProvider.UpdateFinanceArticle(article);
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceArticle(FinanceArticle article)
        {
            dataProvider.DeleteFinanceArticle(article.Id);
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceArticleChanged;

        #endregion

        #region Operation

        public FinanceOperation[] GetFinanceOperations()
        {
            return dataProvider.GetFinanceOperations();
        }

        public void AddFinanceOperation(FinanceOperation operation)
        {
            dataProvider.AddFinanceOperation(operation);
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceOperation(FinanceOperation operation)
        {
            dataProvider.UpdateFinanceOperation(operation);
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceOperation(FinanceOperation operation)
        {
            dataProvider.DeleteFinanceOperation(operation.Id);
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceOperationChanged;

        #endregion
    }
}