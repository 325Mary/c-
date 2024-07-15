using Microsoft.EntityFrameworkCore;
using UserJwtAuthApp.Models;
using Producto.Models;
using Categoria.Models;

namespace UserJwtAuthApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}

namespace Producto.Data
{
    public class ProductoContext : DbContext
    {
        public ProductoContext(DbContextOptions<ProductoContext> options) : base(options) { }

        public DbSet<Producto.Models.Producto> Productos { get; set; }
    }
}


namespace Categoria.Data
{
    public class CategoriaContext : DbContext
    {
        public CategoriaContext(DbContextOptions<CategoriaContext> options) : base(options) { }
        public DbSet<Categoria.Models.Categoria> Categorias { get; set; }
    }
}