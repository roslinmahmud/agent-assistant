using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IVaultRepository
    {
        public Task<Vault> GetVaultAsync(int agentId, DateTime date);
        public Task<Vault> GetVaultByIdAsync(int id);
        public Task<IEnumerable<Vault>> GetVaultListAsync(int agentId, DateTime date);
        public void CreateVault(Vault Vault);
        public void UpdateVault(Vault Vault);
        public Task<int> SaveChangesAsync();
    }
}