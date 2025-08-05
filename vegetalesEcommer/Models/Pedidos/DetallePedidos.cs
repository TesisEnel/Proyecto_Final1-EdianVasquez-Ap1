using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Productos;

namespace Proyecto_Final1.Pedidos
{
    public class DetallePedidos
    {
        [Key]
        public int DetallePedidoId { get; set; } // Clave primaria
        public int PedidoId { get; set; } // Clave foránea para el pedido
        public int ProductoId { get; set; } // Clave foránea para el producto
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public virtual Pedido Pedido { get; set; }
        public virtual Producto Producto { get; set; }
    }
}