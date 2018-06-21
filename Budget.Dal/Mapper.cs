using System.Collections.Generic;
using Budget.Bll.DomainObjects;
using Budget.Dal.Entities;

namespace Budget.Dal
{
    public class Mapper : IMapper
    {
        static AutoMapper.IMapper mapper = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Entities.FinanceArticle, Bll.DomainObjects.FinanceArticle>();
            cfg.CreateMap<Bll.DomainObjects.FinanceArticle, Entities.FinanceArticle>();

            cfg.CreateMap<Entities.FinanceStorage, Bll.DomainObjects.FinanceStorage>();
            cfg.CreateMap<Bll.DomainObjects.FinanceStorage, Entities.FinanceStorage>();
        }).CreateMapper();

        public IEnumerable<Bll.DomainObjects.FinanceArticle> ArticlesDalToBll(IEnumerable<Entities.FinanceArticle> dalArticles)
        {
            return mapper.Map<IEnumerable<Bll.DomainObjects.FinanceArticle>>(dalArticles);
        }

        public Entities.FinanceArticle ArticleBllToDal(Bll.DomainObjects.FinanceArticle bllArticle)
        {
            return mapper.Map<Entities.FinanceArticle>(bllArticle);
        }

        public void ArticleBllToDal(Bll.DomainObjects.FinanceArticle bllArticle, Entities.FinanceArticle dalArticle)
        {
            mapper.Map(bllArticle, dalArticle);
        }

        public IEnumerable<Bll.DomainObjects.FinanceStorage> StoragesDalToBll(IEnumerable<Entities.FinanceStorage> dalStorages)
        {
            return mapper.Map<IEnumerable<Bll.DomainObjects.FinanceStorage>>(dalStorages);
        }

        public Entities.FinanceStorage StorageBllToDal(Bll.DomainObjects.FinanceStorage bllStorage)
        {
            return mapper.Map<Entities.FinanceStorage>(bllStorage);
        }

        public void StorageBllToDal(Bll.DomainObjects.FinanceStorage bllStorage, Entities.FinanceStorage dalStorage)
        {
            mapper.Map(bllStorage, dalStorage);
        }
    }
}