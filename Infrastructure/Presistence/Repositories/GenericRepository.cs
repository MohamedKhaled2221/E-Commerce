using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;

namespace Presistence.Repositories
{
    #region Part 12 Generic Repository
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
        => asNoTracking ? await _dbContext.Set<TEntity>().ToListAsync()
                        : await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();

        public async Task<TEntity?> GetAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);


        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
          =>  await ApplySpecifications(specifications).ToListAsync();
            
        

        public async Task<TEntity?> GetAsync(ISpecifications<TEntity, TKey> specifications)
            => await ApplySpecifications(specifications).FirstOrDefaultAsync();

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> specifications)
        {
            return SpecificationsEvaluator.GetQuery(_dbContext.Set<TEntity>(), specifications);
        }
    }
}

#endregion