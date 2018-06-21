using Budget.Dal.Entities;
using System.Data.Entity;
using System.Data.SQLite;

namespace Budget.Dal.EF
{
    public class BudgetContext : DbContext
    {
        #region Sql.

        public const string CreateFinanceStoragesTableCommand =
@"create table if not exists FinanceStorages(
    Id integer primary key autoincrement not null,
    Name text not null)";

        public const string CreateFinanceArticlesTableCommand =
@"create table if not exists FinanceArticles(
    Id integer primary key autoincrement not null,
    Name text not null,
    Type integer not null)";

        public const string CreateFinanceOperationsTableCommand =
@"create table if not exists FinanceOperations(
    Id integer primary key autoincrement not null,
    Date text not null,
    ArticleId integer not null,
    Sum real not null)";

        #endregion

        public BudgetContext(string nameOrConnectionString)
            : base(new SQLiteConnection(nameOrConnectionString), true)
        {
            Database.Connection.Open();
            Database.ExecuteSqlCommand(CreateFinanceStoragesTableCommand);
            Database.ExecuteSqlCommand(CreateFinanceArticlesTableCommand);
            Database.ExecuteSqlCommand(CreateFinanceOperationsTableCommand);
        }

        public DbSet<FinanceStorage> FinanceStorages { get; set; }
        public DbSet<FinanceArticle> FinanceArticles { get; set; }
        public DbSet<FinanceOperation> FinanceOperations { get; set; }
    }
}