using ContactManagementService.Entities;
using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess.Interfaces
{
    public interface IEntrepriseContactStorageManager
    {
        Task<EntrepriseContactModel> AddEntrepriseContact(EntrepriseContact entrepriseContact);
        Task UpdateEntrepriseContact(EntrepriseContact entrepriseContact);
        Task<EntrepriseContact> GetEntrepriseContact(int entrepriseId, int contactId);
        Task DeleteEntrepriseContact(EntrepriseContact entrepriseContact);
    }
}
