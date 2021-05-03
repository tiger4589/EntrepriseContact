using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.ResultModels
{
    public class EntrepriseResultModel
    {
        public bool IsSuccess { get; set; }
        public int EntrepriseId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
