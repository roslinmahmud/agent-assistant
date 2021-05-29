using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Volt
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public double OpeningLiquidMoney { get; set; }
        public double OpeningCashMoney { get; set; }
        public double ClosingCashMoney { get; set; }
        public double ClosingLiquidMoney { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
