using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T: class
    {
        private readonly AgentContext agentContext;

        public BaseRepository(AgentContext agentContext)
        {
            this.agentContext = agentContext;
        }
        public void Create(T entity)
        {
            agentContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            agentContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return agentContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return agentContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            agentContext.Set<T>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await agentContext.SaveChangesAsync();
        }
    }
}
