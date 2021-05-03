﻿using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.ResultModels
{
    public class EntrepriseContactResultModel
    {
        public bool IsSuccess { get; set; }
        public EntrepriseContactModel EntrepriseContact { get; set; }
        public string ErrorMessage { get; set; }
    }
}
