using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Company
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Company name")]
        public string Name { get; set; }
    }
}
