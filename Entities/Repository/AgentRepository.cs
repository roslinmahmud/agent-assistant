using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class AgentRepository : BaseRepository<Agent>, IAgentRepository
    {
        public AgentRepository(AgentContext agentContext) : base(agentContext)
        {

        }

        public void CreateAgent(Agent agent)
        {
            Create(agent);
        }

        public IEnumerable<Agent> GetAllAgents()
        {
            return FindAll();
        }

        public int SaveChanges()
        {
            return SaveChangees();
        }
    }
}
