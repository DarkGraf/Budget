namespace Budget.Dal.Entities
{
    public class FinanceArticle : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}
