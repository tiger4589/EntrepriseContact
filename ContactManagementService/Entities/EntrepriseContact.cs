using ContactManagementService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Entities
{
    public class EntrepriseContact
    {
        public int EntrepriseId { get; set; }
        public int ContactId { get; set; }
        [ForeignKey(nameof(EntrepriseId))]
        public Entreprise Entreprise { get; set; }
        [ForeignKey(nameof(ContactId))]
        public Contact Contact { get; set; }
        [Required]
        public EContactType ContactType { get; set; }
        public string VATNumber { get; set; }
    }
}
