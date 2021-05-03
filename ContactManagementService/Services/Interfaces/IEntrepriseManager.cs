using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Services.Interfaces
{
    public interface IEntrepriseManager
    {
        Task<int> AddEntreprise(EntrepriseModel model);
        Task UpdateEntreprise(int id, EntrepriseModel model);
        Task DeleteEntreprise(int id);
        Task<bool> IsExistEntreprise(int id);
    }
}
