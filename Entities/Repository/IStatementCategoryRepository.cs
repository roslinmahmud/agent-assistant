using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IStatementCategoryRepository
    {
        Task<IEnumerable<StatementCategory>> GetAllStatementCategoriesAsync(int agentId);
        void CreateStatementCategory(StatementCategory statementCategory);
        void UpdateStatemenCategory(StatementCategory statementCategory);
        Task<int> SaveChangesAsync();
    }
}