using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Package
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "How many persons?")]
        public int Count { get; set; }

        [Display(Name = "Include dinner ")]
        public bool IsDinner { get; set; } = false;

        [ForeignKey("Booth")]
        public int BoothId { get; set; }
        public Booth Booth { get; set; }


        [Required]
        public double Price { get; set; }
    }
}
