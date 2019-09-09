using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace EDDW.Models
{
    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string Fullname { get; set; }

        public string Username { get; set; }

        [Required]
        [ForeignKey("Company")]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

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
        [Display(Name = "Member type")]
        public CompanyMember Type { get; set; } = CompanyMember.Doctor;

        public string MessageToken { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
