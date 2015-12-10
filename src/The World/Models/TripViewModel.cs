using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace The_World.Models
{
    public class TripViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255,MinimumLength= 5)]
        public string Name { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
