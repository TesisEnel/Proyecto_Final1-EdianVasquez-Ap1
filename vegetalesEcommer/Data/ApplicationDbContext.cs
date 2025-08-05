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

        // Aquí se definen los DbSet para tus modelos
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

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.DireccionDeEnvio)
                .WithMany() // Asumiendo que DireccionDeEnvio no tiene una colección de Pedidos
                .HasForeignKey(p => p.DireccionDeEnvioId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Configura la relación entre Pedido y ApplicationUser.
            // Es lógico que si un usuario se elimina, sus pedidos también se eliminen.
            // Aquí mantenemos el comportamiento por defecto de cascada, pero lo explicitamos.
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Configura la relación entre DireccionDeEnvio y ApplicationUser.
            // Es lógico que si un usuario se elimina, sus direcciones de envío también se eliminen.
            // Aquí también se mantiene el comportamiento por defecto de cascada, pero lo explicitamos.
            modelBuilder.Entity<DireccionDeEnvio>()
                .HasOne(d => d.Usuario)
                .WithMany(u => u.DireccionesDeEnvio)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Nota: Con las configuraciones 1 y 2, el ciclo ya está roto.
            // No hay riesgo de conflicto porque la única relación que ahora usa
            // 'Restrict' es la que va de Pedido a DireccionDeEnvio.
            // El resto de las relaciones padre-hijo (User -> Pedidos, User -> Direcciones)
            // pueden usar 'Cascade' sin problema.

            // --- CONFIGURACIÓN DE OTRAS RELACIONES CON ELIMINACIÓN EN CASCADA ---
            // Estas relaciones no causan problemas porque no forman ciclos.

            // Un CarritoItem es un 'hijo' de un Carrito.
            modelBuilder.Entity<CarritoItem>()
                .HasOne(ci => ci.Carrito)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CarritoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Un Carrito es un 'hijo' de un ApplicationUser.
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Carritos)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Un DetallePedidos es un 'hijo' de un Pedido.
            modelBuilder.Entity<DetallePedidos>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Una Valoracion es un 'hijo' de un ApplicationUser.
            modelBuilder.Entity<Valoracion>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Valoraciones)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- SOLUCIÓN PARA LAS ADVERTENCIAS DE PROPIEDADES DECIMALES ---
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
        }
    }
}