using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class StatementCategoryRepository : BaseRepository<StatementCategory>, IStatementCategoryRepository
    {
        public StatementCategoryRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }

        public async Task<IEnumerable<StatementCategory>> GetAllStatementCategoriesAsync(int agentId)
        {
            return await FindByCondition(a => a.AgentId == agentId).
                ToListAsync();
        }

        public void CreateStatementCategory(StatementCategory statementCategory)
        {
            Create(statementCategory);
        }

        public void UpdateStatemenCategory(StatementCategory statementCategory)
        {
            Update(statementCategory);
        }
    }
}
