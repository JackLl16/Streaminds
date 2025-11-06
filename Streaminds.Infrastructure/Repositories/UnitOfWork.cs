using Streaminds.Domain.Contracts;
using Streaminds.Domain.Entities;
using Streaminds.Infrastructure.Data;
using System.Threading.Tasks;
using System;

namespace Streaminds.Infrastructure.Repositories
{
 public class UnitOfWork : IUnitOfWork
 {
 private readonly StreamindsDbContext _context;
 private bool _disposed;

 public IGenericRepository<ProductoStreaming> ProductosStreaming { get; }
 public IGenericRepository<Cliente> Clientes { get; }
 public IGenericRepository<Orden> Ordenes { get; }
 public IGenericRepository<DetalleOrden> DetallesOrden { get; }
 public IGenericRepository<AccesoUsuario> AccesosUsuario { get; }

 public UnitOfWork(StreamindsDbContext context)
 {
 _context = context;
 ProductosStreaming = new GenericRepository<ProductoStreaming>(_context);
 Clientes = new GenericRepository<Cliente>(_context);
 Ordenes = new GenericRepository<Orden>(_context);
 DetallesOrden = new GenericRepository<DetalleOrden>(_context);
 AccesosUsuario = new GenericRepository<AccesoUsuario>(_context);
 }

 public async Task<int> CommitAsync()
 {
 return await _context.SaveChangesAsync();
 }

 public async ValueTask DisposeAsync()
 {
 if (!_disposed)
 {
 await _context.DisposeAsync();
 _disposed = true;
 }
 }
 }
}