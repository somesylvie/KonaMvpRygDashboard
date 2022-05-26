using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace KonaMvpRygDashboard.Database
{
    [ExcludeFromCodeCoverage]
    public class SqliteDatabaseContext : DbContext
    {
        public DbSet<UserStatusEntry> UserStatusEntries { get; set; }

        public SqliteDatabaseContext() : base(Options()) => Database.EnsureCreated();

        private static DbContextOptions<SqliteDatabaseContext> Options()
        {
            var connection = new SqliteConnection("DataSource=datastore.db;Mode=ReadOnly");
            connection.Open();

            return new DbContextOptionsBuilder<SqliteDatabaseContext>().UseSqlite(connection).Options;
        }
    }
}
