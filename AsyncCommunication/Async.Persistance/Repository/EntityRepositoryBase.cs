﻿using Async.Domain.Common.Cashing;
using Async.Domain.Common.Entities;
using Async.Domain.Common.Query;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.Extension;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository;
public abstract class EntityRepositoryBase<TEntity, TContext>(TContext dbContext, ICacheBroker cacheBroker, CacheEntryOptions? cacheEntryOptions = default)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().AsQueryable();

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    protected async ValueTask<TEntity> CreateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (cacheEntryOptions is not null) await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions, cancellationToken);

        if (saveChanges) await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
    protected async ValueTask<IList<TEntity>> GetAsync(QuerySpecification<TEntity> querySpecification, CancellationToken cancellationToken = default)
    {
        var foundEntities = new List<TEntity>();
        var cacheKey = querySpecification.CacheKey;

        if (cacheEntryOptions is null || !await cacheBroker.TryGetAsync<List<TEntity>>(cacheKey, out var cachedEntities, cancellationToken))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();

            if (querySpecification.AsNoTracking) initialQuery = initialQuery.AsNoTracking();
            initialQuery = initialQuery.ApplySpecification(querySpecification);
            foundEntities = await initialQuery.ToListAsync(cancellationToken);
            if (cacheEntryOptions is not null) await cacheBroker.SetAsync(cacheKey, foundEntities, cacheEntryOptions, cancellationToken);
        }
        else if (cachedEntities is not null)
        {
            foundEntities = cachedEntities;
        }

        return foundEntities;
    }
    protected async ValueTask<TEntity?> GetByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }
    protected async ValueTask<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }
}