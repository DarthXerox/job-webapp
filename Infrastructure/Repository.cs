using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Repository<TEntity> where TEntity : BaseEntity
    {
        private readonly JobDbContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(JobDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        /// <summary>
        ///     Not a CRUD operation
        ///     This will be removed, once the unit tests are rewritten
        /// </summary>
        public virtual IQueryable<TEntity> GetAll() => context.Set<TEntity>();

        public virtual TEntity GetById(int id) => dbSet.Find(id);

        public virtual async Task<TEntity> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public virtual void Add([NotNull] TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Add)} entity must not be null");
            dbSet.Add(entity);
        }

        public virtual async Task<TEntity> AddAsync([NotNull] TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            await context.AddAsync(entity);
            return entity;
        }

        public virtual void Update([NotNull] TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Update)} entity must not be null");
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            Delete(dbSet.Find(id));
        }

        public virtual void Delete([NotNull] TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException($"{nameof(Delete)} entity must not be null");
            if (context.Entry(entity).State == EntityState.Detached) dbSet.Attach(entity);
            dbSet.Remove(entity);
        }
    }
}
