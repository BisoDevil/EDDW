using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class Programme
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start date & time")]
        public DateTime StartDate { get; set; }


        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End date & time")]
        public DateTime EndDate { get; set; }


        [Required]
        [ForeignKey("Speaker")]
        [Display(Name = "Speaker")]
        public Guid SpeakerId { get; set; }
        public Speaker Speaker { get; set; }

        [Required]
        [ForeignKey("Room")]
        [Display(Name = "Choose a Room")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        [Required]
        [Display(Name = "Attendance code")]
        public string AttendanceCode { get; set; }

    }
}
