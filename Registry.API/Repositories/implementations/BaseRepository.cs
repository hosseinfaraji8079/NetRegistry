using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registry.API.Common;
using Registry.API.Data;
using Registry.API.Repositories.Interfaces;

namespace Registry.API.Repositories.implementations;

public class BaseRepository<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly RegistryDbContext _dbContext;

    public BaseRepository(RegistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> GetQueryableAsync(bool disableTracking = true)
    {
        return _dbContext.Set<T>().AsQueryable();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (disableTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, bool disableTracking = true)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (disableTracking)
            query = query.AsNoTracking();

        return await query.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeString = null,
        bool disableTracking = true)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (disableTracking)
            query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString))
            query = query.Include(includeString);

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        IEnumerable<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true)
    {
        var query = _dbContext.Set<T>().AsQueryable();

        if (disableTracking)
            query = query.AsNoTracking();

        if (includes != null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate != null)
            query = query.Where(predicate);

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> GetByIdAsync(long id, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString))
            query = query.Include(includeString);

        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }


    public async Task<T> GetByIdAsync(long id, IEnumerable<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking)
            query = query.AsNoTracking();

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        entity.CreateDate = DateTime.UtcNow;
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.ModifiedDate = DateTime.UtcNow;
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        entity.IsDelete = true;
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        var query = _dbContext.Set<T>().AsQueryable();
        if (predicate != null)
            query = query.Where(predicate);

        return await query.CountAsync();
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbContext.Set<T>().AnyAsync(predicate);
    }
}