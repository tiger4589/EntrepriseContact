using ContactManagementService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess.Interfaces
{
    public interface IEntrepriseAddressStorageManager
    {
        Task<int> AddAddress(EntrepriseAddress address);
        Task UpdateAddress(EntrepriseAddress address);
        Task<EntrepriseAddress> GetAddress(int id);
        Task DeleteAddress(EntrepriseAddress address);
        Task<List<EntrepriseAddress>> GetEntrepriseAddresses(int entrepriseId); 
    }
}
