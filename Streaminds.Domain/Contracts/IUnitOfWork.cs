using System.Threading.Tasks;
using Streaminds.Domain.Entities;

namespace Streaminds.Domain.Contracts
{
 public interface IUnitOfWork : IAsyncDisposable
 {
 IGenericRepository<ProductoStreaming> ProductosStreaming { get; }
 IGenericRepository<Cliente> Clientes { get; }
 IGenericRepository<Orden> Ordenes { get; }
 IGenericRepository<DetalleOrden> DetallesOrden { get; }
 IGenericRepository<AccesoUsuario> AccesosUsuario { get; }
 Task<int> CommitAsync();
 }
}
