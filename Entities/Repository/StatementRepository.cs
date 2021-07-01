using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class StatementRepository : BaseRepository<Statement>, IStatementRepository
    {
        public StatementRepository(AgentContext agentContext): base(agentContext)
        {

        }
        public void CreateStatement(Statement statement)
        {
            Create(statement);
        }

        public IEnumerable<Statement> GetAllStatements()
        {
            return FindAll();
        }

        public void UpdateStatement(Statement statement)
        {
            Update(statement);
        }
    }
}
