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
        public void CreateStatement(Statement statement);
        public IEnumerable<Statement> GetAllStatements();
        public void UpdateStatement(Statement statement);
        public int SaveChanges();
    }
}
