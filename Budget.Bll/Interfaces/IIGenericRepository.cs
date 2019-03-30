namespace Budget.Bll.Interfaces
{
    public interface IGenericRepository<TEntity>
         where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(object id);

        TEntity[] GetAll();

        void Update(TEntity entity);

        TEntity[] Filter(IExpressionSpecification<TEntity> specification);
    }
}