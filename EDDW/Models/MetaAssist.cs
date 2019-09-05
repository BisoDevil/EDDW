using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class MetaAssist
    {
        public int Id { get; set; }

        [Required]
        public Guid User { get; set; }

        [Display(Name = "Accompodation Date")]
        [DataType(DataType.Date)]
        public DateTime AccoStartDate { get; set; }


        [Display(Name = "Accompodation Date")]
        [DataType(DataType.Date)]
        public DateTime AccoEndDate { get; set; }

        [Display(Name = "With transportation")]
        public bool IsTransportation { get; set; }

        [Display(Name = "Room type")]
        public RoomType Room { get; set; } = RoomType.Single;
    }
}
