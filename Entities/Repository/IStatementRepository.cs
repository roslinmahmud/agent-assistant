using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IStatementRepository
    {
        public Task<IEnumerable<Statement>> GetAllStatementsAsync(string agentId, DateTime dateTime);
        public void CreateStatement(Statement statement);
        public void UpdateStatement(Statement statement);
        public Task<int> SaveChangesAsync();
    }
}
