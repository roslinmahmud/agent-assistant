using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Entities.Repository
{
    public interface IVoltRepository
    {
        public Task<Volt> GetVoltAsync(int agentId, DateTime date);
        public Task<Volt> GetVoltByIdAsync(int id);
        public Task<IEnumerable<Volt>> GetVoltListAsync(int agentId, DateTime date);
        public void CreateVolt(Volt volt);
        public void UpdateVolt(Volt volt);
        public Task<int> SaveChangesAsync();
    }
}