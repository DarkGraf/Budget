using Budget.Dal.Entities;
using System.Data.Entity;

namespace Budget.Dal.EF
{
    class BudgetContext : DbContext
    {
        public BudgetContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }

        public DbSet<FinanceStorage> FinanceStorages { get; set; }
    }
}