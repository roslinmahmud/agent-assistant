using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Agent>> GetAllAgents()
        {
            return await FindAll()
                .ToListAsync();
        }

    }
}
