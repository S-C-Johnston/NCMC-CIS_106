using System.Data.Common;
using final.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

// yoinked from
// https://www.meziantou.net/testing-ef-core-in-memory-using-sqlite.htm
public class BookContextFactory : IDisposable
{
    private DbConnection? _connection;

    private DbContextOptions<BookContext> CreateOptions(DbConnection my_connection)
    {
        return new DbContextOptionsBuilder<BookContext>()
            .UseSqlite(my_connection).Options;
    }

    public BookContext CreateContext()
    {
        if (_connection == null)
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = CreateOptions(_connection);
            using (var context = new BookContext(options))
            {
                context.Database.EnsureCreated();
            }
        }

        return new BookContext(CreateOptions(_connection));
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            _connection.Dispose();
            _connection = null;
        }
    }
}