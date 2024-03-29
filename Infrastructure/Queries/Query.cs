using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Queries
{
    public abstract class Query<TEntity> where TEntity : BaseEntity
    {
        protected IQueryable<TEntity> queryable;
        protected JobDbContext dbContext;

        protected Query(JobDbContext dbContext)
        {
            this.dbContext = dbContext;
            queryable = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> ExecuteAsync()
        {
            var ret = await queryable.ToListAsync() ?? new List<TEntity>();
            queryable = dbContext.Set<TEntity>();
            return ret;
        }

        public IEnumerable<TEntity> Execute()
        {
            return queryable.ToList() ?? new List<TEntity>();
        }

        public Query<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ascendingOrder = true)
        {
            queryable = ascendingOrder
                ? queryable.OrderBy(keySelector)
                : queryable.OrderByDescending(keySelector);

            return this;
        }

        public Query<TEntity> Page(int pageSize, int pageNumber)
        {
            // pages numbered from 1
            queryable = queryable.Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);
            return this;
        }
    }
}
