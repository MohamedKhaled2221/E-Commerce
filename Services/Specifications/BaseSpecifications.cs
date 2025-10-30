using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    #region Part 7 Speceifications Implementation 
    internal abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region Criteria
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;

        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
        #endregion



        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
        // p => p.ProductBrand , p=> p.ProductType
        protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        #endregion
        #region Part 1 Sorting [ OrderBy , OrderByDescendng ]


        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
        protected void SetOrderBy(Expression<Func<TEntity, object>> orderByExpression) // OrderBy(p=>p.Name)
           => OrderBy = orderByExpression;

        protected void SetOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression) 
           => OrderByDescending = orderByDescExpression;

        #endregion
        #region Part 3 , 4 , 5 Pagination
        public int Skip {get; private set;}

        public int Take { get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination (int pageIndex , int PageSize)
        {
            IsPaginated =true;
            Take = PageSize;
            Skip = (pageIndex - 1) * PageSize;
        }

        #endregion

    } 
    #endregion
}
