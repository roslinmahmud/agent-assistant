using Entities.Models;
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
        public void CreateVolt(Volt volt)
        {
            Create(volt);
        }

        public IEnumerable<Volt> GetAllVolts()
        {
            return FindAll();
        }

        public void UpdateVolt(Volt volt)
        {
            Update(volt);
        }
    }
}
