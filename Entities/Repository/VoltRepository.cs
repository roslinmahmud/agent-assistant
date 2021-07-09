using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class VoltRepository : BaseRepository<Volt>, IVoltRepository
    {
        public VoltRepository(AgentContext agentContext) : base(agentContext)
        {

        }

        public async Task<Volt> GetVoltByIdAsync(int id)
        {
            return await FindByCondition(v => v.Id == id).
                FirstOrDefaultAsync();
        }

        public async Task<Volt> GetVoltAsync(int agentId, DateTime date)
        {
            return await FindByCondition(v => v.AgentId == agentId && v.Date.Date == date.Date).
                FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Volt>> GetVoltListAsync(int agentId, DateTime date)
        {
            return await FindByCondition(v => v.AgentId == agentId && date.Month == v.Date.Month && v.Date.Year == date.Year).
                ToListAsync();
        }

        public void CreateVolt(Volt volt)
        {
            Create(volt);
        }

        public void UpdateVolt(Volt volt)
        {
            Update(volt);
        }

    }
}
