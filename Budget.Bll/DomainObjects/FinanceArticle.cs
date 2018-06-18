using Budget.Bll.DomainObjects.Enums;

namespace Budget.Bll.DomainObjects
{
    public class FinanceArticle
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FinanceArticleType Type { get; set; }
    }
}