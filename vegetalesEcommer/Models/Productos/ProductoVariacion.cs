using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final1.Productos
{
    public class ProductoVariacion
    {
        [Key]
        public int VariacionId { get; set; }
        public int ProductoId { get; set; }
        public string NombreVariacion { get; set; }
        public string ValorVariacion { get; set; }
        public decimal PrecioAdicional { get; set; }

        // Propiedad de navegación
        public virtual Producto Producto { get; set; }
    }
}