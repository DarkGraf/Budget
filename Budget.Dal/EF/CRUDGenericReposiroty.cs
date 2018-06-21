using Budget.Dal.Entities;
using Budget.Dal.Mappers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Budget.Dal.EF
{
    class CRUDGenericReposiroty<TDal, TBll>
            where TDal : class, IEntity
    {
        readonly DbContext context;
        readonly IParticularMapper<TBll, TDal> mapper;
        readonly IDbSet<TDal> set;

        public CRUDGenericReposiroty(DbContext context, IParticularMapper<TBll, TDal> mapper)
        {
            this.context = context;
            this.mapper = mapper;

            set = context.Set<TDal>();
        }

        public void Add(TBll entity)
        {
            TDal dal = mapper.MapBllToDal(entity);
            set.Add(dal);
        }

        public void Delete(long id)
        {
            TDal dal = set.Find(id);
            if (dal != null)
            {
                set.Remove(dal);
            }
        }

        public TBll[] GetAll(params Expression<Func<TDal, object>>[] includes)
        {
            IQueryable<TDal> query = set;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            TDal[] dal = query.ToArray();
            return mapper.MapDalToBll(dal).ToArray();
        }

        public void Update(TBll entity)
        {
            TDal dal = mapper.MapBllToDal(entity);
            dal = set.Find(dal.Id);
            if (dal != null)
            {
                mapper.MapBllToDal(entity, dal);
            }
        }
    }
}