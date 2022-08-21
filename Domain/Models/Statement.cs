﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Statement : Entity
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public StatementCategory Category { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}
