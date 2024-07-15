using System.Collections.Generic;
using Producto.Models;

namespace Producto.Services
{
    public interface IProductoService
    {
        IEnumerable<Producto.Models.Producto> GetAllProductos();
        Producto.Models.Producto GetProductoById(int id);
        void AddProducto(Producto.Models.Producto producto);
        void UpdateProducto(Producto.Models.Producto producto);
        void DeleteProducto(int id);
    }
}

