using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.ResultModels
{
    public class ContactResultModel
    {
        public bool IsSuccess { get; set; }
        public int ContactId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
