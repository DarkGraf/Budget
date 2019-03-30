using Budget.Bll.DomainObjects;
using Budget.Bll.Interfaces;
using System.Data.Entity;
using System.Data.SQLite;

namespace Budget.Dal.EF
{
    public class BudgetContext : DbContext, IBudgetUnitOfWork
    {
        ISpecificationFactory specificationFactory;

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

            FinanceArticleRepository = new CRUDGenericReposiroty<FinanceArticle>(this);
            FinanceOperationRepository = new CRUDGenericReposiroty<FinanceOperation>(this);
            FinanceStorageRepository = new CRUDGenericReposiroty<FinanceStorage>(this);
        }

        public DbSet<FinanceStorage> FinanceStorages { get; set; }
        public DbSet<FinanceArticle> FinanceArticles { get; set; }
        public DbSet<FinanceOperation> FinanceOperations { get; set; }

        #region IBudgetUnitOfWork

        public IGenericRepository<FinanceArticle> FinanceArticleRepository { get; }

        public IGenericRepository<FinanceOperation> FinanceOperationRepository { get; }

        public IGenericRepository<FinanceStorage> FinanceStorageRepository { get; }

        public ISpecificationFactory GetSpecificationFactory()
        {
            if (specificationFactory == null)
            {
                specificationFactory = new SpecificationFactory();
            }

            return specificationFactory;
        }

        public void Save()
        {
            SaveChanges();
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FinanceOperation>()
                .HasRequired(p => p.Article)
                .WithMany()
                .Map(m => m.MapKey("ArticleId"));
        }
    }
}