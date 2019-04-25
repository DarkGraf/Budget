using Budget.Bll.Interfaces;
using System;

namespace Budget.Bll.DomainObjects
{
    public sealed class BudgetObject
    {
        readonly IBudgetUnitOfWork unitOfWork;

        public BudgetObject(IBudgetUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #region Storage

        public FinanceStorage[] GetFinanceStorage()
        {
            return unitOfWork.FinanceStorageRepository.GetAll();
        }

        public void AddFinanceStorage(FinanceStorage storage)
        {
            unitOfWork.FinanceStorageRepository.Add(storage);
            unitOfWork.Save();
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            unitOfWork.FinanceStorageRepository.Update(storage);
            unitOfWork.Save();
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceStorage(FinanceStorage storage)
        {
            unitOfWork.FinanceStorageRepository.Delete(storage.Id);
            unitOfWork.Save();
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceStoragesChanged;

        #endregion

        #region Article

        public FinanceArticle[] GetFinanceArticle()
        {
            return unitOfWork.FinanceArticleRepository.GetAll();
        }

        public void AddFinanceArticle(FinanceArticle article)
        {
            unitOfWork.FinanceArticleRepository.Add(article);
            unitOfWork.Save();
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceArticle(FinanceArticle article)
        {
            unitOfWork.FinanceArticleRepository.Update(article);
            unitOfWork.Save();
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceArticle(FinanceArticle article)
        {
            unitOfWork.FinanceArticleRepository.Delete(article.Id);
            unitOfWork.Save();
            FinanceArticleChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceArticleChanged;

        #endregion

        #region Operation

        public FinanceOperation[] GetFinanceOperations()
        {
            return unitOfWork.FinanceOperationRepository.Filter(unitOfWork.GetSpecificationFactory().GetOperationsWithArticles());
        }

        public void AddFinanceOperation(FinanceOperation operation)
        {
            unitOfWork.FinanceOperationRepository.Add(operation);
            unitOfWork.Save();
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceOperation(FinanceOperation operation)
        {
#warning Если обновить любое поле кроме статьи, будет ошибка - Проверить.
            unitOfWork.FinanceOperationRepository.Update(operation);
            unitOfWork.Save();
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceOperation(FinanceOperation operation)
        {
            unitOfWork.FinanceOperationRepository.Delete(operation);
            unitOfWork.Save();
            FinanceOperationChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceOperationChanged;

        #endregion
    }
}