using ContactManagementService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess.Interfaces
{
    public interface IEntrepriseStorageManager
    {
        Task<int> AddEntreprise(Entreprise entreprise);
        Task UpdateEntreprise(Entreprise entreprise);
        Task<Entreprise> GetEntreprise(int id);
        Task DeleteEntreprise(Entreprise entreprise);
    }
}
