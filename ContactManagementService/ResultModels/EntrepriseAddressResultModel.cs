using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.ResultModels
{
    public class EntrepriseAddressResultModel
    {
        public bool IsSuccess { get; set; }
        public int AddressId { get; set; }
        public string ErrorMessage { get; set; }
        public string WarningMessage { get; set; }
    }
}
