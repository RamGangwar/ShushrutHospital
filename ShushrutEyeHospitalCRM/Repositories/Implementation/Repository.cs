using Microsoft.EntityFrameworkCore;
using ShushrutEyeHospitalCRM.ApplicationContext;
using ShushrutEyeHospitalCRM.Repositories.Interface;
using ShushrutEyeHospitalCRM.Resources;
using System.Linq.Expressions;

namespace ShushrutEyeHospitalCRM.Repositories.Implementation
{
    public class Repository<T> : IRepositories<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> CreateAsync(T entity)
        {
            _dbContext.Add<T>(entity);
            int res = await _dbContext.SaveChangesAsync();
            return res > 0 ? CommonResource.CreateSuccess : CommonResource.CreateFailed;
        }

        public async Task<string> DeleteAsync(T entity)
        {
            _dbContext.Remove<T>(entity);
            return await _dbContext.SaveChangesAsync() > 1 ? CommonResource.DeleteSuccess : CommonResource.DeleteFailed;
        }

        public async Task<string> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.Update<T>(entity);
            return await _dbContext.SaveChangesAsync() > 1 ? CommonResource.UpdateSuccess : CommonResource.UpdateFailed;
        }
        public async Task<IQueryable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            return await Task.FromResult(_dbContext.Set<T>().Where(predicate));
        }
        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbContext.Set<T>().AsQueryable().Where(expression).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsQueryable().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Id)
        {
            return await _dbContext.Set<T>().FindAsync(Id);
        }
    }
}
