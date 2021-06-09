using Entities.Models;
using System.Collections.Generic;

namespace Entities.Repository
{
    public interface IVoltRepository
    {
        public void CreateVolt(Volt volt);
        public IEnumerable<Volt> GetAllVolts();
        public void UpdateVolt(Volt volt);
        public int SaveChanges();
    }
}