using Budget.Bll.DomainObjects;
using System;

namespace Budget.Bll.Interfaces
{
    public interface IBudgetUnitOfWork : IDisposable
    {
        IGenericRepository<FinanceArticle> FinanceArticleRepository { get; }
        IGenericRepository<FinanceOperation> FinanceOperationRepository { get; }
        IGenericRepository<FinanceStorage> FinanceStorageRepository { get; }
        void Save();

        ISpecificationFactory GetSpecificationFactory();
    }
}