using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Services.Interfaces
{
    public interface IEntrepriseAddressManager
    {
        Task<int> AddEntrepriseAddress(EntrepriseAddressModel model);
        Task UpdateEntrepriseAddress(int id, EntrepriseAddressModel model);
        Task DeleteEntrepriseAddress(int id);
        Task<bool> CheckIfEntrepriseHasMainAddress(int entrepriseId);
        Task<bool> CheckIfEntrepriseHasMainAddressFromAddressId(int addressId);
        Task<int> GetEntrepriseIdFromAdressId(int addressId);
    }
}
