using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Queries
{
    public abstract class Query<TEntity> where TEntity : BaseEntity
    {
        protected IQueryable<TEntity> query;

        protected Query(JobDbContext dbContext)
        {
            query = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            return await query?.ToListAsync() ?? new List<TEntity>();
        }

        public Query<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascendingOrder = true)
        {
            query = ascendingOrder
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);

            return this;
        }
    }
}
