using System.Collections.Generic;

namespace Budget.Dal.Mappers
{
    public class Mapper : IMapper
    {
        static AutoMapper.IMapper mapper = new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Entities.FinanceArticle, Bll.DomainObjects.FinanceArticle>();
            cfg.CreateMap<Bll.DomainObjects.FinanceArticle, Entities.FinanceArticle>();

            cfg.CreateMap<Entities.FinanceStorage, Bll.DomainObjects.FinanceStorage>();
            cfg.CreateMap<Bll.DomainObjects.FinanceStorage, Entities.FinanceStorage>();

            cfg.CreateMap<Entities.FinanceOperation, Bll.DomainObjects.FinanceOperation>();
            cfg.CreateMap<Bll.DomainObjects.FinanceOperation, Entities.FinanceOperation>()
                .ForMember("Article", opt => opt.MapFrom(src => (Entities.FinanceArticle)null)); ;
        }).CreateMapper();

        public IEnumerable<Bll.DomainObjects.FinanceArticle> MapDalToBll(IEnumerable<Entities.FinanceArticle> dalArticles)
        {
            return mapper.Map<IEnumerable<Bll.DomainObjects.FinanceArticle>>(dalArticles);
        }

        public Entities.FinanceArticle MapBllToDal(Bll.DomainObjects.FinanceArticle bllArticle)
        {
            return mapper.Map<Entities.FinanceArticle>(bllArticle);
        }

        public void MapBllToDal(Bll.DomainObjects.FinanceArticle bllArticle, Entities.FinanceArticle dalArticle)
        {
            mapper.Map(bllArticle, dalArticle);
        }

        public IEnumerable<Bll.DomainObjects.FinanceStorage> MapDalToBll(IEnumerable<Entities.FinanceStorage> dalStorages)
        {
            return mapper.Map<IEnumerable<Bll.DomainObjects.FinanceStorage>>(dalStorages);
        }

        public Entities.FinanceStorage MapBllToDal(Bll.DomainObjects.FinanceStorage bllStorage)
        {
            return mapper.Map<Entities.FinanceStorage>(bllStorage);
        }

        public void MapBllToDal(Bll.DomainObjects.FinanceStorage bllStorage, Entities.FinanceStorage dalStorage)
        {
            mapper.Map(bllStorage, dalStorage);
        }

        public IEnumerable<Bll.DomainObjects.FinanceOperation> MapDalToBll(IEnumerable<Entities.FinanceOperation> dalSource)
        {
            return mapper.Map<IEnumerable<Bll.DomainObjects.FinanceOperation>>(dalSource);
        }

        public Entities.FinanceOperation MapBllToDal(Bll.DomainObjects.FinanceOperation bllSource)
        {
            return mapper.Map<Entities.FinanceOperation>(bllSource);
        }

        public void MapBllToDal(Bll.DomainObjects.FinanceOperation bllSorce, Entities.FinanceOperation dalDestination)
        {
            mapper.Map(bllSorce, dalDestination);
        }
    }
}