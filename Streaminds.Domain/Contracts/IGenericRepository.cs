using System.Collections.Generic;
using System.Threading.Tasks;

namespace Streaminds.Domain.Contracts
{
 public interface IGenericRepository<T> where T : class
 {
 Task<T?> GetByIdAsync(int id);
 Task<IEnumerable<T>> GetAll();
 Task AddAsync(T entity);
 Task Update(T entity);
 Task Remove(T entity);
 Task<int> SaveChangesAsync();
 }
}
