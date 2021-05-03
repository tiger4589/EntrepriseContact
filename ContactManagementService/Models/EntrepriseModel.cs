using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Models
{
    public class EntrepriseModel
    {
        [Required]
        public string Name { get; set; }
        public string Sector { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string VATNumber { get; set; }
    }
}
