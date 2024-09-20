using Microsoft.AspNetCore.Mvc;
using Producto.Services;
using Productos.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Producto.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public IActionResult GetAllProductos()
        {
            var productos = _productoService.GetAllProductos();
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductoById(int id)
        {
            var producto = _productoService.GetProductoById(id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(producto);
        }

        [HttpPost]
        public IActionResult AddProducto(Productos.Models.Productos producto)
        {
            _productoService.AddProducto(producto);
            return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, Productos.Models.Productos producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }
            _productoService.UpdateProducto(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            _productoService.DeleteProducto(id);
            return NoContent();
        }
    }
}
