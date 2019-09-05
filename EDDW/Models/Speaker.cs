using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace EDDW.Models
{
    public class Speaker
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }

        public string Username { get; set; }


        [Required]
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]

        [Display(Name = "phone number")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public Title Title { get; set; } = Title.Mr;
        public string Speciality { get; set; }

        [Required]
        public string Country { get; set; }
    }
}
