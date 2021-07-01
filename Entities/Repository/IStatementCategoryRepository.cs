using Entities.Models;
using System.Collections.Generic;

namespace Entities.Repository
{
    public interface IStatementCategoryRepository
    {
        void CreateStatementCategory(StatementCategory statementCategory);
        IEnumerable<StatementCategory> GetAllStatementCategories();
        void UpdateStatemenCategory(StatementCategory statementCategory);
        public int SaveChanges();
    }
}