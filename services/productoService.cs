using System;
using System.Collections.Generic;
using System.Linq;
using Productos.Models;
using Producto.Data;

namespace Producto.Services
{
    public class ProductoService : IProductoService
    {


        private readonly ProductoContext _context;

        public ProductoService(ProductoContext context)
        {
            _context = context;
        }

        public IEnumerable<Productos.Models.Productos> GetAllProductos()
        {
            return _context.Productos.ToList();
        } 

        public Productos.Models.Productos GetProductoById(int id)
        {
            var producto= _context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                throw new ($"EL producto con el {id} no se ecnontro");
            }
            return producto;
        }

        public void AddProducto(Productos.Models.Productos producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();
        }

        public void UpdateProducto(Productos.Models.Productos producto)
        {
            var ProductoExistente = GetProductoById(producto.Id);
            if (ProductoExistente != null)
            {
                ProductoExistente.NombreProducto = producto.NombreProducto;
                ProductoExistente.Cantidad = producto.Cantidad;
                ProductoExistente.Precio = producto.Precio;
                _context.SaveChanges();
            }
        }

        public void DeleteProducto(int id)
        {
            var producto = GetProductoById(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                _context.SaveChanges();
            }
        }
    }
}
