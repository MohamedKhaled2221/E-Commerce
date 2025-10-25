using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Presistence
{
    #region Part 8 Specifications Evaluator 
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery //_dbContext.Set<Product>()
            , ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;  //_dbContext.Set<Product>()
            // Apply criteria
            if (specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria); // query = _dbContext.Set<Product>().Where(p => p.id = id)
            }
            if (specifications.IncludeExpressions?.Count > 0)
            {

                query = specifications.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
            }
            return query;
        }
    } 
    #endregion
}
