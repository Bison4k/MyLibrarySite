using Microsoft.EntityFrameworkCore;
using MyLibrary.Models;

namespace MyLibrary.DataBase;


public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options)
    {
    }
    
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasKey(x => x.Id);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS ;Database=MyLibrary;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}