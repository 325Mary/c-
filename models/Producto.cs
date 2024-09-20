namespace Productos.Models
{
    public class Productos
    {
        public int Id { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
