using ContactManagementService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Models
{
    public class EntrepriseContactModel
    {
        [Required]
        public int EntrepriseId { get; set; }
        [Required]
        public int ContactId { get; set; }
        [Required]
        public EContactType ContactType { get; set; }
        public string VATNumber { get; set; }
    }
}
