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

    public class ApplicationUser : IdentityUser
    {

        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string Apellido { get; set; }

        public DateTime? FechaNacimiento { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime? UltimoLogin { get; set; }

        public bool Activo { get; set; } = true;

        public virtual ICollection<DireccionDeEnvio> DireccionesDeEnvio { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }

        public virtual ICollection<Carrito> Carritos { get; set; }
        public virtual ICollection<Valoracion> Valoraciones { get; set; }
    }
}
