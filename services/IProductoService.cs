using System.Collections.Generic;
using Productos.Models;

namespace Producto.Services
{
    public interface IProductoService
    {
        IEnumerable<Productos.Models.Productos> GetAllProductos();
        Productos.Models.Productos GetProductoById(int id);
        void AddProducto(Productos.Models.Productos producto);
        void UpdateProducto(Productos.Models.Productos producto);
        void DeleteProducto(int id);
    }
}

