using Microsoft.EntityFrameworkCore;
using PosterrPosts.Infra;
using PosterrPosts.Infra.Contracts.Repositories;
using PosterrPosts.Test.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PosterrPosts.Test.Mocks.Repositories
{
    internal class BaseRepositoryMock<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        protected readonly PosterrPostDbContext _dbContext;

        public BaseRepositoryMock()
        {
            var dbContext = DbContextHelper.GetDatabaseContext().Result;
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            var find = await GetAll(predicate);

            return find.Count();
        }

        public async Task<T> Get(TKey id)
            => await _dbSet.FindAsync(id);

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAll()
            => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task Add(T entity)
            => await _dbSet.AddAsync(entity);

        public void SaveChanges()
            => _dbContext.SaveChanges();
    }
}
