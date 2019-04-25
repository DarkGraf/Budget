using Budget.Bll.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace Budget.Dal.EF
{
    class CRUDGenericReposiroty<TEntity> : IGenericRepository<TEntity>
            where TEntity : class
    {
        readonly DbContext context;
        readonly IDbSet<TEntity> set;

        public CRUDGenericReposiroty(DbContext context)
        {
            this.context = context;
            set = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            set.Add(entity);
        }

        public void Delete(object id)
        {
            TEntity entity = set.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            set.Remove(entity);
        }

        public TEntity[] GetAll()
        {
            return set.ToArray();
        }

        public void Update(TEntity entity)
        {
            set.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public TEntity[] Filter(IExpressionSpecification<TEntity> specification)
        {
            IQueryable<TEntity> entities = set;

            if (specification.Includes.Length > 0)
            {
                foreach (string include in specification.Includes)
                {
                    entities = entities.Include(include);
                }
            }

            IQueryable<TEntity> query = entities.Where(specification.ToExpression());

            return query.ToArray();
        }
    }
}