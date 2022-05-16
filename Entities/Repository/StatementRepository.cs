using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class StatementRepository : BaseRepository<Statement>, IStatementRepository
    {
        public StatementRepository(ApplicationContext applicationContext) : base(applicationContext)
        {

        }

        public async Task<IEnumerable<Statement>> GetAllStatementsAsync(int agentId, DateTime dateTime)
        {
            return await FindByCondition(s => s.AgentId == agentId && s.Date.Month == dateTime.Month && s.Date.Year == dateTime.Year).
                ToListAsync();
        }

        public void CreateStatement(Statement statement)
        {
            Create(statement);
        }

        public void UpdateStatement(Statement statement)
        {
            Update(statement);
        }
    }
}
