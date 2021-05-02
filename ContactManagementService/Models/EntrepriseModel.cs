using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Models
{
    public class EntrepriseModel
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string VATNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public bool IsMainAddress { get; set; }
    }
}
