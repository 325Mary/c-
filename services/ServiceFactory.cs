using Producto.Data;
using Categoria.Data;

namespace Producto.Services
{
    public static class ServiceFactory
    {
        public static IProductoService CreateProductoService(ProductoContext context)
        {
            return new ProductoService(context);
        }
       
    }
}

namespace Categoria.Services
{
    public static class ServiceFactory
    {
        public static ICategoriaService CreateCategoriaService(CategoriaContext context)
        {
            return new CategoriaService(context);
        }
    }
}