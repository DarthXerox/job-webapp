using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public abstract class Query<TEntity> where TEntity : BaseEntity
    {
        protected IQueryable<TEntity> Queryable;

        protected Query(DbContext dbContext)
        {
            Queryable = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync() => await Queryable.ToListAsync() ?? new List<TEntity>();

        public Query<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascendingOrder = true)
        {
            Queryable = ascendingOrder
                ? Queryable.OrderBy(keySelector)
                : Queryable.OrderByDescending(keySelector);

            return this;
        }
    }
}
