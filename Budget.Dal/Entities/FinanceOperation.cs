using System;

namespace Budget.Dal.Entities
{
    public class FinanceOperation
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long ArticleId { get; set; }
        public FinanceArticle Article { get; set; }
        public decimal Sum { get; set; }
    }
}
