using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Pedidos;
using Proyecto_Final1.Carritos;
using Proyecto_Final1.Productos;
using Proyecto_Final1.Usuarios;

namespace Proyecto_Final1.Data
{
    // Heredamos de IdentityUser para obtener las propiedades base de un usuario
    public class ApplicationUser : IdentityUser
    {
        // --- Propiedades personalizadas para el usuario ---

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? UltimoLogin { get; set; }

        // Propiedad para marcar al usuario como activo o inactivo
        public bool Activo { get; set; } = true;


        // --- Propiedades de navegaci�n para las relaciones con otras tablas ---

        // Relaci�n con las direcciones de env�o (uno a muchos)
        public virtual ICollection<DireccionDeEnvio> DireccionesDeEnvio { get; set; }

        // Relaci�n con los pedidos (uno a muchos)
        public virtual ICollection<Pedido> Pedidos { get; set; }

        // Relaci�n con los carritos (uno a muchos)
        public virtual ICollection<Carrito> Carritos { get; set; }

        // Relaci�n con las valoraciones de productos (uno a muchos)
        public virtual ICollection<Valoracion> Valoraciones { get; set; }
    }
}
