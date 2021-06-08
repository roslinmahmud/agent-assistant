using Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee: ApplicationUser
    {
        [Required]
        public string AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
