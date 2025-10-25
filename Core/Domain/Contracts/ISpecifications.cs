using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Property Signature For each & Every spec [Where , Include , etc..]
        // Criteria --> where(p => p.Id)
        public Expression<Func<TEntity,bool>>? Criteria { get; } // p => p.Id

        // Includes --> Include(p => p.Category)

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

    }
}
