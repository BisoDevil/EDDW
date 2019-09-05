using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Booth
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Room name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Room location on floor plan")]
        public string Location { get; set; }

    }
}
