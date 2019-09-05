using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey("Programme")]
        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; }


    }
}
