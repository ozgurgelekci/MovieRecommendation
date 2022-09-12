using MovieRecommendation.Domain.Common;
using System.Linq.Expressions;

namespace MovieRecommendation.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetPagedList(int pageNumber, int pageSize);
        Task<List<T>> GetAsync();
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<T> AddAsync(T model);
        T Update(T model);
        Task BulkInsertAsync(List<T> model);
        Task<T> RemoveAsync(T model);
    }
}
