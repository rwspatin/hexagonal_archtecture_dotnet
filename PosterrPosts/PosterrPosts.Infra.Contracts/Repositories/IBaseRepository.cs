using System.Linq.Expressions;

namespace PosterrPosts.Infra.Contracts.Repositories
{
    public interface IBaseRepository<T, TKey>
    {
        Task<T> Get(TKey id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<int> Count(Expression<Func<T, bool>> predicate);
        Task Add(T item);
        void SaveChanges();
    }
}
