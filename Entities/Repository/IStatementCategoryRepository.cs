using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IStatementCategoryRepository
    {
        Task<IEnumerable<StatementCategory>> GetAllStatementCategoriesAsync(string agentId);
        void CreateStatementCategory(StatementCategory statementCategory);
        void UpdateStatemenCategory(StatementCategory statementCategory);
        Task<int> SaveChangesAsync();
    }
}