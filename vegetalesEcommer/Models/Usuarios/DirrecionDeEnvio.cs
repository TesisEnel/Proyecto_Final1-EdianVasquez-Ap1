using System.ComponentModel.DataAnnotations;
using Proyecto_Final1.Data;
using Proyecto_Final1.Usuarios; // Asegúrate de que esta referencia sea correcta

namespace Proyecto_Final1.Usuarios
{
    public class DireccionDeEnvio
    {
        [Key]
        public int DireccionId { get; set; }
        public string UsuarioId { get; set; } // Id de ApplicationUser
        [Required]
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public bool EsDefault { get; set; }

        // Propiedad de navegación, ahora de tipo ApplicationUser
        public virtual ApplicationUser Usuario { get; set; }
    }
}
