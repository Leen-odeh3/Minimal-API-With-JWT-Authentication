namespace MinimalAPIWithJWTAuthentication.Api.Abstracts;

public interface IRepo<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
}
