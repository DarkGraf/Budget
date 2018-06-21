using System.Collections.Generic;

namespace Budget.Dal
{
    public interface IMapper
    {
        IEnumerable<Bll.DomainObjects.FinanceArticle> ArticlesDalToBll(IEnumerable<Entities.FinanceArticle> dalArticles);
        Entities.FinanceArticle ArticleBllToDal(Bll.DomainObjects.FinanceArticle bllArticle);
        void ArticleBllToDal(Bll.DomainObjects.FinanceArticle bllArticle, Entities.FinanceArticle dalArticle);

        IEnumerable<Bll.DomainObjects.FinanceStorage> StoragesDalToBll(IEnumerable<Entities.FinanceStorage> dalStorages);
        Entities.FinanceStorage StorageBllToDal(Bll.DomainObjects.FinanceStorage bllStorage);
        void StorageBllToDal(Bll.DomainObjects.FinanceStorage bllStorage, Entities.FinanceStorage dalStorage);
    }
}