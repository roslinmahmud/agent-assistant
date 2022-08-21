using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IStatementRepository
    {
        public Task<IEnumerable<Statement>> GetAllStatementsAsync(int agentId, DateTime dateTime);
        public void CreateStatement(Statement statement);
        public void UpdateStatement(Statement statement);
        public Task<int> SaveChangesAsync();
    }
}
