using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Sponsor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Inforamtion { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Logo { get; set; }
    }
}
