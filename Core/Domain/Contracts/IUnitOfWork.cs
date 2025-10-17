using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        //CompleteAsync, SaveChangesAsync()
        public Task<int> SaveChangesAsync();
        // Signature for Method will return an Object From Class That Implements IGenericRepository<TEntity,TKey>

        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
        // New Generic Repository<Product, int>();
        // New Generic Repository<ProductType, int>();
        // New Generic Repository<ProductBrand, int>();
    }
}
