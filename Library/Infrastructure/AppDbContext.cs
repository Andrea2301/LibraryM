using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set;}
    public DbSet<Book> Books { get; set;}
    public DbSet<Loan> Loan { get; set;}
    public DbSet<History> History { get; set;}

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){} 
}

// dotnet ef migrations add initials
// dotnet ef database update 
//dotnet restore o dotnet build