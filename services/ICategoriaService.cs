using System.Collections.Generic;
using Categoria.Models;

namespace Categoria.Services
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria.Models.Categoria> GetAllCategorias();

        Categoria.Models.Categoria GetCategoriaById(int id);

        void AddCategoria(Categoria.Models.Categoria categoria);

        void UpdateCategoria(Categoria.Models.Categoria categoria);

        void DeleteCategoria(int id);
    }
}

