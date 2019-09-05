using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Programme")]

        public int ProgrammeId { get; set; }
        public Programme Programme { get; set; }

        [DataType(DataType.Upload)]
        public string Link { get; set; }
    }
}
