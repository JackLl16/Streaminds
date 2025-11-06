using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Streaminds.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Streaminds.Infrastructure.Data
{
 public static class DbSeeder
 {
 public static async Task SeedAsync(StreamindsDbContext context)
 {
 if (context == null) throw new ArgumentNullException(nameof(context));

 // Ensure database is created by migrations step prior to calling Seed
 // Seed ProductosStreaming
 if (!await context.ProductoStreamings.AnyAsync())
 {
 var productos = new List<ProductoStreaming>
 {
 new ProductoStreaming { Nombre = "Película A", Descripcion = "Película de acción", TipoProducto = "Pelicula", Precio =9.99m, DuracionDias =30, CalidadVideo = "HD", Activo = true },
 new ProductoStreaming { Nombre = "Serie B - Temporada1", Descripcion = "Temporada completa", TipoProducto = "Serie", Precio =19.99m, DuracionDias =365, CalidadVideo = "FullHD", Activo = true },
 new ProductoStreaming { Nombre = "Concierto Live", Descripcion = "Concierto en vivo", TipoProducto = "Evento", Precio =4.99m, DuracionDias =7, CalidadVideo = "HD", Activo = true }
 };

 await context.ProductoStreamings.AddRangeAsync(productos);
 }

 // Seed Clientes
 if (!await context.Clientes.AnyAsync())
 {
 var clientes = new List<Cliente>
 {
 new Cliente { Email = "juan@example.com", NombreCompleto = "Juan Perez", FechaRegistro = DateTime.UtcNow.AddDays(-10), Activo = true },
 new Cliente { Email = "maria@example.com", NombreCompleto = "Maria Gomez", FechaRegistro = DateTime.UtcNow.AddDays(-5), Activo = true }
 };

 await context.Clientes.AddRangeAsync(clientes);
 }

 // Persist if there are pending changes
 if (context.ChangeTracker.HasChanges())
 {
 await context.SaveChangesAsync();
 }
 }
 }
}
