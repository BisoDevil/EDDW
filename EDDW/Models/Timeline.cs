using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Timeline
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Information { get; set; }

        [Required]
        [ForeignKey("Speaker")]
        public Guid SpeakerId { get; set; }
        public Speaker Speaker { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
