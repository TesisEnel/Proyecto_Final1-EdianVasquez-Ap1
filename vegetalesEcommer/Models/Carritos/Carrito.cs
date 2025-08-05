using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Data;
using Proyecto_Final1.Usuarios; // Agregado para usar ApplicationUser

namespace Proyecto_Final1.Carritos
{
    public class Carrito
    {
        [Key]
        public int CarritoId { get; set; }
        public string UsuarioId { get; set; } // El tipo de Id de IdentityUser es string por defecto
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

        // Propiedad de navegación para los items del carrito
        public virtual ICollection<CarritoItem> Items { get; set; }

        // Propiedad de navegación para el usuario, ahora ApplicationUser
        public virtual ApplicationUser Usuario { get; set; }
    }
}