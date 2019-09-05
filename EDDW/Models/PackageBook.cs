using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EDDW.Models
{
    public class PackageBook
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Package")]
        public int PackageId { get; set; }
        public Package Package { get; set; }
        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
