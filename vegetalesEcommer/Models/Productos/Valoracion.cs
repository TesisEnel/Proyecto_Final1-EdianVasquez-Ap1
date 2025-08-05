using System;
using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Data;
using Proyecto_Final1.Usuarios; // Agregado para usar ApplicationUser

namespace Proyecto_Final1.Productos
{
    public class Valoracion
    {
        [Key]
        public int ValoracionId { get; set; }
        public int ProductoId { get; set; }
        public string UsuarioId { get; set; } // Id de ApplicationUser
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaValoracion { get; set; }

        // Propiedades de navegación
        public virtual Producto Producto { get; set; }
        public virtual ApplicationUser Usuario { get; set; }
    }
}