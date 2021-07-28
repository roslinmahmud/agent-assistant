using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public class VaultRepository : BaseRepository<Vault>, IVaultRepository
    {
        public VaultRepository(AgentContext agentContext) : base(agentContext)
        {

        }

        public async Task<Vault> GetVaultByIdAsync(int id)
        {
            return await FindByCondition(v => v.Id == id).
                FirstOrDefaultAsync();
        }

        public async Task<Vault> GetVaultAsync(int agentId, DateTime date)
        {
            return await FindByCondition(v => v.AgentId == agentId && v.Date.Date == date.Date).
                FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Vault>> GetVaultListAsync(int agentId, DateTime date)
        {
            return await FindByCondition(v => v.AgentId == agentId && date.Month == v.Date.Month && v.Date.Year == date.Year).
                ToListAsync();
        }

        public void CreateVault(Vault Vault)
        {
            Create(Vault);
        }

        public void UpdateVault(Vault Vault)
        {
            Update(Vault);
        }

    }
}
