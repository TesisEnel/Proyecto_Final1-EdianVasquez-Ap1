using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Productos;


namespace Proyecto_Final1.Carritos
{
    public class CarritoItem
    {
        [Key]
        public int CarritoItemId { get; set; } // Clave primaria
        public int CarritoId { get; set; } // Clave foránea para el carrito
        public int ProductoId { get; set; } // Clave foránea para el producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public virtual Carrito Carrito { get; set; }
        public virtual Producto Producto { get; set; }
    }
}