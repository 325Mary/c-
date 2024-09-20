using Microsoft.AspNetCore.Mvc;
using Categoria.Services;
using Categoria.Models;

namespace Categoria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public IActionResult GetAllCategorias()
        {
            var categorias = _categoriaService.GetAllCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id)
        {
            var categoria = _categoriaService.GetCategoriaById(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult AddCategoria(Models.Categoria categoria)
        {
            _categoriaService.AddCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategoria(int id, Models.Categoria categoria)  
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }
            _categoriaService.UpdateCategoria(categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            _categoriaService.DeleteCategoria(id);
            return NoContent();
        }
    }
}
