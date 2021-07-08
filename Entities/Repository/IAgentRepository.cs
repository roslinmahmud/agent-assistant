using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IAgentRepository
    {
        public void CreateAgent(Agent agent);
        public Task<IEnumerable<Agent>> GetAllAgents();
        public Task<int> SaveChanges();
    }
}