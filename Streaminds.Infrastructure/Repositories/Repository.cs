using Microsoft.EntityFrameworkCore;
using Streaminds.Infrastructure.Data;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Streaminds.Domain.Contracts;

namespace Streaminds.Infrastructure.Repositories
{
 public class GenericRepository<T> : IGenericRepository<T> where T : class
 {
 private readonly StreamindsDbContext _context;
 private readonly DbSet<T> _dbSet;

 public GenericRepository(StreamindsDbContext context)
 {
 _context = context;
 _dbSet = context.Set<T>();
 }

 public async Task AddAsync(T entity)
 {
 await _dbSet.AddAsync(entity);
 }

 public async Task<IEnumerable<T>> GetAll()
 {
 return await _dbSet.ToListAsync();
 }

 public async Task<T?> GetByIdAsync(int id)
 {
 return await _dbSet.FindAsync(id);
 }

 public async Task<int> SaveChangesAsync()
 {
 return await _context.SaveChangesAsync();
 }

 public Task Update(T entity)
 {
 _dbSet.Update(entity);
 return Task.CompletedTask;
 }

 public Task Remove(T entity)
 {
 _dbSet.Remove(entity);
 return Task.CompletedTask;
 }
 }
}