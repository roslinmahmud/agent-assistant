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

        public void UpdateAgent(Agent agent)
        {
            Update(agent);
        }

        public async Task<Agent> GetAgent(int Id)
        {
            return await FindByCondition(a => a.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Agent>> GetAllAgents()
        {
            return await FindAll()
                .ToListAsync();
        }

    }
}
