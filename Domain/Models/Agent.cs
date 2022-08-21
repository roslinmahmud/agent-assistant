using Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Agent : Entity
    {
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
