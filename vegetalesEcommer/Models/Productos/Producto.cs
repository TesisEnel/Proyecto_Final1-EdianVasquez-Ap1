using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Final1.Productos
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public string ImagenUrl { get; set; }
        public int Stock { get; set; }

        // Propiedades de navegación
        public virtual ICollection<ProductoVariacion> Variaciones { get; set; }
        public virtual ICollection<Valoracion> Valoraciones { get; set; }
    }
}