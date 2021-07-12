using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Agent: Entity
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
