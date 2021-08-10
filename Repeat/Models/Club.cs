using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Repeat.Models
{
    public class Club
    {
        [Key]
        [Required]
        public int ClubId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public ICollection<Event> Events { get; set; }

        public static implicit operator Club(Event v)
        {
            throw new NotImplementedException();
        }
    }
}
