using System.Collections.Generic;

namespace Budget.Dal.Mappers
{
    public interface IParticularMapper<TBll, TDal>
    {
        IEnumerable<TBll> MapDalToBll(IEnumerable<TDal> dalSource);
        TDal MapBllToDal(TBll bllSource);
        void MapBllToDal(TBll bllSorce, TDal dalDestination);
    }
}