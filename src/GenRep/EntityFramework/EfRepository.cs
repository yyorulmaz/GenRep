﻿using GenRep.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GenRep.EntityFramework
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        protected readonly Func<DbContext> _dbContext;
        public EfRepository(Func<DbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        #region SELECT
        public async Task<TEntity> SelectByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
        }
        public async Task<List<TEntity>> SelectAllAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                if (filter == null)
                    return await context.Set<TEntity>().ToListAsync(cancellationToken);
                else
                    return await context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken);
            }
        }



        public async Task<TEntity> OrderBy(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().OrderBy(filter).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<TEntity>> OrderByList(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().OrderBy(filter).ToListAsync(cancellationToken);
        }
        public async Task<TEntity> OrderByDescending(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().OrderByDescending(filter).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<TEntity>> OrderByDescendingList(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
                return await context.Set<TEntity>().OrderByDescending(filter).ToListAsync(cancellationToken);
        }
        #endregion

        #region INSERT
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                await context.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<List<TEntity>> InsertRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                return entities;
            }
        }
        #endregion

        #region UPDATE
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                context.Update(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                context.UpdateRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return entities;
            }
        }
        #endregion

        #region DELETE
        public async Task<TEntity> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                var entity = SelectByIdAsync(id, cancellationToken).Result;
                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        public async Task<bool> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = _dbContext())
            {
                context.RemoveRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        #endregion
    }

    public class EfRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        #region SELECT
        public async Task<TEntity> SelectByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
        }
        public async Task<List<TEntity>> SelectAllAsync(Expression<Func<TEntity, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                if (filter == null)
                    return await context.Set<TEntity>().ToListAsync(cancellationToken);
                else
                    return await context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken);
            }
        }


        public async Task<TEntity> OrderBy(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().OrderBy(filter).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<TEntity>> OrderByList(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().OrderBy(filter).ToListAsync(cancellationToken);
        }
        public async Task<TEntity> OrderByDescending(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().OrderByDescending(filter).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<List<TEntity>> OrderByDescendingList(Expression<Func<TEntity, object>> filter, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
                return await context.Set<TEntity>().OrderByDescending(filter).ToListAsync(cancellationToken);
        }
        #endregion

        #region INSERT
        public async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                await context.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<List<TEntity>> InsertRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return entities;
            }
        }
        #endregion

        #region UPDATE
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                context.Update(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                context.UpdateRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return entities;
            }
        }
        #endregion

        #region DELETE
        public async Task<TEntity> DeleteAsync(object id, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                var entity = SelectByIdAsync(id, cancellationToken).Result;
                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return entity;
            }
        }
        public async Task<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                context.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        public async Task<bool> DeleteRangeAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            using (var context = new TContext())
            {
                context.RemoveRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        #endregion
    }
}
