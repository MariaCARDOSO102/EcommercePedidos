using EcommercePedidos.Data.Interfaces;
using EcommercePedidos.Objects.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommercePedidos.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task GerarPedido(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();   
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            var entityId = _context.Entry(entity).Property("Id").CurrentValue;

            var trackedEntity = _context.ChangeTracker.Entries<Pedido>()
                .FirstOrDefault(e => e.Property("Id").CurrentValue.Equals(entityId));

            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await SaveChanges();
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
