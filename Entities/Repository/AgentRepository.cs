using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            return FindAll()
                .Include(a=>a.StatementCategories);
        }

    }
}
