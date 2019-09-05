using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class BoothBook
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Sponsor")]

        public int SponsorId { get; set; }
        public Sponsor Sponsor { get; set; }

        [Required]
        [ForeignKey("Booth")]
        public int BoothId { get; set; }
        public Booth Booth { get; set; }
    }
}
