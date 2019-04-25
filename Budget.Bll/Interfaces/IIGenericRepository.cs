namespace Budget.Bll.Interfaces
{
    public interface IGenericRepository<TEntity>
         where TEntity : class
    {
        void Add(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entity);

        TEntity[] GetAll();

        void Update(TEntity entity);

        TEntity[] Filter(IExpressionSpecification<TEntity> specification);
    }
}