using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public TEntity Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.AddRange(entities);
                context.SaveChanges();
                return entities;
            }
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {
                await context.AddRangeAsync(entities);
                await context.SaveChangesAsync();
                return entities;
            }
        }

        public int Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }
        public async Task<int> DeleteAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                return await context.SaveChangesAsync();
            }
        }

        public TEntity Find(int id)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public async Task<TEntity> FindAsync(int id)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async Task<List<TEntity>> GetAllAsync (Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return  filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entities);
                context.SaveChanges();
                return entities;
            }
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entities);
                await context.SaveChangesAsync();
                return entities;
            }
        }
    }
}
