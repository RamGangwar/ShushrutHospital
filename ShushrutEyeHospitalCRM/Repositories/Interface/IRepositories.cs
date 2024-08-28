using System.Linq.Expressions;

namespace ShushrutEyeHospitalCRM.Repositories.Interface
{
    public interface IRepositories<T> where T : class
    {
        Task<string> CreateAsync(T entity);
        Task<string> UpdateAsync(T entity);
        Task<string> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IQueryable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int Id);
    }
}
