using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class StatementCategory : Entity
    {
        [Required]
        public string CategoryName { get; set; }
        public bool IsIncome { get; set; }

        [Required]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
