namespace Budget.Dal.Mappers
{
    public interface IMapper : IParticularMapper<Bll.DomainObjects.FinanceArticle, Entities.FinanceArticle>,
        IParticularMapper<Bll.DomainObjects.FinanceStorage, Entities.FinanceStorage>,
        IParticularMapper<Bll.DomainObjects.FinanceOperation, Entities.FinanceOperation> { }
}