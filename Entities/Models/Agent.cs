using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Agent: ApplicationUser
    {
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Volt> Volts { get; set; }
    }
}
