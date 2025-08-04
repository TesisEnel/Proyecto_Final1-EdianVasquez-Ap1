using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_Final1.Usuarios;
using Proyecto_Final1.Carritos;
using Proyecto_Final1.Pedidos;
using Proyecto_Final1.Productos;

namespace Proyecto_Final1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aqu� se definen los DbSet para tus modelos
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<CarritoItem> CarritoItems { get; set; }
        public DbSet<DetallePedidos> DetallePedidos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<ProductoVariacion> ProductoVariaciones { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DbSet<DireccionDeEnvio> DireccionesDeEnvio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Soluci�n para el error de "multiple cascade paths" ---
            // 1. Configura la relaci�n entre Pedido y DireccionDeEnvio para NO CASCADE.
            // Esto significa que si se elimina una Direcci�nDeEnvio, los Pedidos asociados NO se eliminar�n autom�ticamente.
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.DireccionDeEnvio)
                .WithMany() // Asumiendo que DireccionDeEnvio no tiene una colecci�n de Pedidos
                .HasForeignKey(p => p.DireccionDeEnvioId)
                .OnDelete(DeleteBehavior.NoAction); // <-- �Esta es una parte clave!

            // 2. Configura la relaci�n entre DireccionDeEnvio y ApplicationUser para NO CASCADE.
            // Esto significa que si se elimina un ApplicationUser, sus DireccionesDeEnvio NO se eliminar�n autom�ticamente.
            // Esta es la segunda parte clave para romper el ciclo de eliminaci�n en cascada.
            // Tendr�s que manejar la eliminaci�n de direcciones manualmente si un usuario se borra.
            modelBuilder.Entity<DireccionDeEnvio>()
                .HasOne(d => d.Usuario)
                .WithMany() // Asumiendo que ApplicationUser no tiene una colecci�n de DireccionesDeEnvio
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.NoAction); // <-- �Esta es la otra parte clave!

            // La relaci�n entre Pedido y ApplicationUser puede mantener CASCADE,
            // ya que no forma parte del ciclo problem�tico con DireccionDeEnvio.
            // Si un ApplicationUser se elimina, sus Pedidos asociados s� se eliminar�n.
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade); // Comportamiento por defecto, expl�cito para claridad

            // --- Soluci�n para las advertencias de propiedades decimales ---
            // Especifica la precisi�n y escala para todas las propiedades decimales.
            // Un valor com�n para moneda es (18, 2), que significa 18 d�gitos en total, con 2 despu�s del punto decimal.
            modelBuilder.Entity<CarritoItem>()
                .Property(ci => ci.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetallePedidos>()
                .Property(dp => dp.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductoVariacion>()
                .Property(pv => pv.PrecioAdicional)
                .HasPrecision(18, 2);

            // Aqu� puedes agregar configuraciones adicionales para el modelo si es necesario.
        }
    }
}
