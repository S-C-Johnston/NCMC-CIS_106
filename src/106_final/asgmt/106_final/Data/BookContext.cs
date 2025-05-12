using final.Models;
using Microsoft.EntityFrameworkCore;

namespace final.Data;

public class BookContext : DbContext
{
    public BookContext(DbContextOptions<BookContext> options): base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
}