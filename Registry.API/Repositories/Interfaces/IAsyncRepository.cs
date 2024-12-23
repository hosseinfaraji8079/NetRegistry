using System.Linq.Expressions;
using Registry.API.Common;

namespace Registry.API.Repositories.Interfaces;

public interface IAsyncRepository<T> where T : EntityBase
{
    /// <summary>
    /// Retrieves all entities without any filtering or tracking.
    /// </summary>
    /// <param name="disableTracking">Disable EF Core tracking if true.</param>
    /// <returns>List of all entities.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(bool disableTracking = true);

    /// <summary>
    /// Retrieves entities based on a filtering expression.
    /// </summary>
    /// <param name="predicate">Filtering condition.</param>
    /// <param name="disableTracking">Disable EF Core tracking if true.</param>
    /// <returns>List of filtered entities.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate, bool disableTracking = true);

    /// <summary>
    /// Retrieves entities with optional filtering, ordering, and eager loading.
    /// </summary>
    /// <param name="predicate">Filtering condition.</param>
    /// <param name="orderBy">Ordering logic.</param>
    /// <param name="includeString">Navigation properties to include as a comma-separated string.</param>
    /// <param name="disableTracking">Disable EF Core tracking if true.</param>
    /// <returns>List of entities matching the specified criteria.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeString = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves entities with optional filtering, ordering, and eager loading using expressions.
    /// </summary>
    /// <param name="predicate">Filtering condition.</param>
    /// <param name="orderBy">Ordering logic.</param>
    /// <param name="includes">Navigation properties to include as expressions.</param>
    /// <param name="disableTracking">Disable EF Core tracking if true.</param>
    /// <returns>List of entities matching the specified criteria.</returns>
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        IEnumerable<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true);

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the entity.</param>
    /// <returns>The entity matching the specified id or null if not found.</returns>
    Task<T> GetByIdAsync(long id);

    /// <summary>
    /// Adds a new entity asynchronously.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    /// <returns>The added entity.</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Updates an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Deletes an existing entity asynchronously.
    /// </summary>
    /// <param name="entity">Entity to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Counts the total number of entities matching a predicate.
    /// </summary>
    /// <param name="predicate">Filtering condition.</param>
    /// <returns>Number of matching entities.</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);

    /// <summary>
    /// Checks whether any entity matches the specified predicate.
    /// </summary>
    /// <param name="predicate">Filtering condition.</param>
    /// <returns>True if any entity matches; otherwise, false.</returns>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
}