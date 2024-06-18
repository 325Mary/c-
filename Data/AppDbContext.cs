// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using UserJwtAuthApp.Models;

namespace UserJwtAuthApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
