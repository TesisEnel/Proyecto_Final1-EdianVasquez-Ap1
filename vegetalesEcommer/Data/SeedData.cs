using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Proyecto_Final1.Usuarios; // Asegúrate de que esta referencia sea correcta

namespace Proyecto_Final1.Data
{
    public static class SeedData // La clase ahora es estática
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            try
            {
                // 1. Crear Roles si no existen
                await EnsureRolesCreated(roleManager);

                // 2. Crear Usuario Admin por defecto
                await EnsureAdminUserExists(userManager, roleManager);

                // 3. Crear Usuario Cliente de prueba
                await EnsureTestClientExists(userManager, roleManager);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al inicializar datos: {ex.Message}");
            }
        }

        private static async Task EnsureRolesCreated(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Cliente", "Vendedor" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                    Console.WriteLine($"Rol '{roleName}' creado exitosamente.");
                }
            }
        }

        private static async Task EnsureAdminUserExists(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminEmail = "admin@Vegetales.com";
            var adminPassword = "AdminPassword321!";
            var adminRole = "Admin";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Nombre = "Administrador",
                    Apellido = "Vegetales",
                    FechaRegistro = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    if (await roleManager.RoleExistsAsync(adminRole))
                    {
                        await userManager.AddToRoleAsync(adminUser, adminRole);
                        Console.WriteLine($"Usuario admin '{adminEmail}' creado exitosamente.");
                    }
                }
                else
                {
                    throw new Exception($"Error al crear usuario admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private static async Task EnsureTestClientExists(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var clientEmail = "cliente@Vegetales.com";
            var clientPassword = "ClientPassword123!";
            var clientRole = "Cliente";

            var clientUser = await userManager.FindByEmailAsync(clientEmail);

            if (clientUser == null)
            {
                clientUser = new ApplicationUser
                {
                    UserName = clientEmail,
                    Email = clientEmail,
                    EmailConfirmed = true,
                    Nombre = "Cliente",
                    Apellido = "Prueba",
                    FechaRegistro = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(clientUser, clientPassword);

                if (result.Succeeded)
                {
                    if (await roleManager.RoleExistsAsync(clientRole))
                    {
                        await userManager.AddToRoleAsync(clientUser, clientRole);
                        Console.WriteLine($"Usuario cliente '{clientEmail}' creado exitosamente.");
                    }
                }
                else
                {
                    Console.WriteLine($"Error al crear usuario cliente: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}