using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Services.Interfaces
{
    public interface IEntrepriseContactManager
    {
        Task<EntrepriseContactModel> AddEntrepriseContact(EntrepriseContactModel model);
        Task UpdateEntrepriseContact(EntrepriseContactModel model);
        Task DeleteEntrepriseContract(int entrepriseId, int contactId);
    }
}
