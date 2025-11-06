using Microsoft.EntityFrameworkCore;
using Streaminds.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Streaminds.Infrastructure.Data
{
 public class StreamindsDbContext : IdentityDbContext
 {
 public StreamindsDbContext(DbContextOptions<StreamindsDbContext> options) : base(options)
 {
 }

 public DbSet<ProductoStreaming> ProductoStreamings { get; set; } = null!;
 public DbSet<Cliente> Clientes { get; set; } = null!;
 public DbSet<Orden> Ordenes { get; set; } = null!;
 public DbSet<DetalleOrden> DetalleOrdenes { get; set; } = null!;
 public DbSet<AccesoUsuario> AccesosUsuarios { get; set; } = null!;

 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
 base.OnModelCreating(modelBuilder);

 // ProductoStreaming
 modelBuilder.Entity<ProductoStreaming>(entity =>
 {
 entity.ToTable("ProductoStreamings");
 entity.HasKey(e => e.Id);
 entity.Property(e => e.Nombre).IsRequired().HasMaxLength(200);
 entity.Property(e => e.Descripcion).HasMaxLength(1000);
 entity.Property(e => e.TipoProducto).IsRequired().HasMaxLength(100);
 entity.Property(e => e.Precio).HasPrecision(18,2);
 entity.Property(e => e.DuracionDias);
 entity.Property(e => e.CalidadVideo).HasMaxLength(50);
 entity.Property(e => e.Activo).IsRequired();
 });

 // Cliente
 modelBuilder.Entity<Cliente>(entity =>
 {
 entity.ToTable("Clientes");
 entity.HasKey(e => e.Id);
 entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
 entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(200);
 entity.Property(e => e.FechaRegistro).IsRequired();
 entity.Property(e => e.Activo).IsRequired();
 });

 // Orden
 modelBuilder.Entity<Orden>(entity =>
 {
 entity.ToTable("Ordenes");
 entity.HasKey(e => e.Id);
 entity.Property(e => e.Fecha).IsRequired();
 entity.Property(e => e.Total).HasPrecision(18,2);
 entity.Property(e => e.Estado).IsRequired().HasMaxLength(50);
 entity.HasOne(o => o.Cliente).WithMany(c => c.Ordenes).HasForeignKey(o => o.ClienteId).OnDelete(DeleteBehavior.Cascade);
 });

 // DetalleOrden
 modelBuilder.Entity<DetalleOrden>(entity =>
 {
 entity.ToTable("DetalleOrdenes");
 entity.HasKey(e => e.Id);
 entity.Property(e => e.Cantidad).IsRequired();
 entity.Property(e => e.PrecioUnitario).HasPrecision(18,2);
 entity.Property(e => e.Subtotal).HasPrecision(18,2);
 entity.HasOne(d => d.Orden).WithMany(o => o.DetalleOrdenes).HasForeignKey(d => d.OrdenId).OnDelete(DeleteBehavior.Cascade);
 entity.HasOne(d => d.ProductoStreaming).WithMany(p => p.DetallesOrden).HasForeignKey(d => d.ProductoStreamingId).OnDelete(DeleteBehavior.Restrict);
 });

 // AccesoUsuario
 modelBuilder.Entity<AccesoUsuario>(entity =>
 {
 entity.ToTable("AccesosUsuarios");
 entity.HasKey(e => e.Id);
 entity.Property(e => e.FechaActivacion).IsRequired();
 entity.Property(e => e.FechaExpiracion).IsRequired();
 entity.HasOne(a => a.Cliente).WithMany(c => c.Accesos).HasForeignKey(a => a.ClienteId).OnDelete(DeleteBehavior.Cascade);
 entity.HasOne(a => a.ProductoStreaming).WithMany(p => p.AccesosUsuarios).HasForeignKey(a => a.ProductoStreamingId).OnDelete(DeleteBehavior.Restrict);
 });
 }
 }
}