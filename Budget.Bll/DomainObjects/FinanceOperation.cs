using System;

namespace Budget.Bll.DomainObjects
{
    public class FinanceOperation
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public FinanceArticle Article { get; set; }
        public decimal Sum { get; set; }
    }
}