using Entities.Models;
using System.Collections.Generic;

namespace Entities.Repository
{
    public interface IAgentRepository
    {
        public void CreateAgent(Agent agent);
        public IEnumerable<Agent> GetAllAgents();
        public int SaveChanges();
    }
}