using System;
using System.Collections.Generic;
using System.Linq;
using Categoria.Models;
using Categoria.Data;

namespace Categoria.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly CategoriaContext _context;

        public CategoriaService(CategoriaContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria.Models.Categoria> GetAllCategorias()
        {
            return _context.Categorias.ToList();
        }

        public Categoria.Models.Categoria GetCategoriaById(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria  == null)
            {
                 throw new Exception($"La categoria con el {id} no fue encontrada");
            }
            return categoria;
        }

        public void AddCategoria(Categoria.Models.Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
        }

        public void UpdateCategoria(Categoria.Models.Categoria categoria)
        {
            var categoriaExistente = GetCategoriaById(categoria.Id);
            if (categoriaExistente != null)
            {
                categoriaExistente.NombreCategoria = categoria.NombreCategoria;
                _context.SaveChanges();
            }
        }

        public void DeleteCategoria(int id)
        {
            var categoria = GetCategoriaById(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
            }
        }
    }
}
