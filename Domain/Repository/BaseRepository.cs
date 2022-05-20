using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationContext applicationContext;

        public BaseRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public void Create(T entity)
        {
            applicationContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            applicationContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return applicationContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return applicationContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            applicationContext.Set<T>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await applicationContext.SaveChangesAsync();
        }
    }
}
