using Entities.Models;
using System.Collections.Generic;

namespace Entities.Repository
{
    public class StatementCategoryRepository : BaseRepository<StatementCategory>, IStatementCategoryRepository
    {
        public StatementCategoryRepository(AgentContext agentContext) : base(agentContext)
        {

        }
        public void CreateStatementCategory(StatementCategory statementCategory)
        {
            Create(statementCategory);
        }

        public IEnumerable<StatementCategory> GetAllStatementCategories()
        {
            return FindAll();
        }

        public void UpdateStatemenCategory(StatementCategory statementCategory)
        {
            Update(statementCategory);
        }
    }
}
