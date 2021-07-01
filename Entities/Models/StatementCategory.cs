using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class StatementCategory: Entity
    {
        [Required]
        public string CategoryName { get; set; }
        public bool IsIncome { get; set; }

        [Required]
        public string AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
