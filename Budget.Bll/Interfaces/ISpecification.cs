namespace Budget.Bll.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T o);
    }
}