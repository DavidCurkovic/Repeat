using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repeat.Models
{
    public class Event
    {
        [Key]
        [Required]
        public int EventId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Tickets { get; set; }
        [Required]
        public DateTime Beginning { get; set; }
        [Required]
        public DateTime Ending { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ClubId { get; set; }
        [Required]
        public int ImageId { get; set; }
    }

}
