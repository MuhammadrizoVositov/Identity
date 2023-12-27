using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserRole.Domain.Entites;

namespace UserRole.Persistance.Repositoryies;
public abstract class EntityBaseRepository<TEntity,TContext> where TEntity : User 
    where TContext : DbContext
{
    protected TContext DbContext;
    public EntityBaseRepository(TContext dbContext)
    {
        DbContext = dbContext;
    }

    protected IQueryable<TEntity> Get(bool asNoTracking = default)
    {
        if(asNoTracking)
            DbContext.Set<TEntity>().AsNoTracking();

        return DbContext.Set<TEntity>();
    }
    protected async ValueTask<TEntity>GetByIdasync(Guid id,bool asNoTracking = default,CancellationToken cancellationToken=default)
    {
        if (asNoTracking)
            DbContext.Set<TEntity>().AsNoTracking();

        return await DbContext.Set<TEntity>().SingleOrDefaultAsync(a => a.Id == id, cancellationToken);

    }
    protected async ValueTask<TEntity>CreateAsync(TEntity entity,bool saveChanges=true,CancellationToken cancellationToken=default)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            if(saveChanges )
            await DbContext.SaveChangesAsync(cancellationToken);
            return entity;
    }
    protected async ValueTask<TEntity> Update(TEntity entity,bool saveChanges=true,CancellationToken cancellationToken=default)
    {
        DbContext.Update(entity);
        if(saveChanges)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
           
        }
        return entity;
    }
    protected async ValueTask<TEntity> DeleteAsync(TEntity entity ,bool saveChanges=true,CancellationToken cancellationToken=default)
    {
        DbContext.Remove(entity);
        if (saveChanges )
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
        return entity;
    }
        

        
        

}
