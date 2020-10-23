using AmmoFinder.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace AmmoFinder.Persistence.UnitTests.Services
{
    public abstract class SqlLiteContext : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ProductsContext DbContext;

        protected SqlLiteContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ProductsContext>()
                    .UseSqlite(_connection)
                    .Options;
            DbContext = new ProductsContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
