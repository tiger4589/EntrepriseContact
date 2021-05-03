using AutoMapper;
using ContactManagementService.Entities;
using ContactManagementService.Models;
using ContactManagementService.Services.Interfaces;
using ContactManagementService.StorageAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Services
{
    public class EntrepriseAddressManager : IEntrepriseAddressManager
    {
        private readonly IEntrepriseAddressStorageManager _storageManager;
        private readonly IMapper _mapper;

        public EntrepriseAddressManager(IEntrepriseAddressStorageManager storageManager, IMapper mapper)
        {
            _storageManager = storageManager;
            _mapper = mapper;
        }

        public async Task<int> AddEntrepriseAddress(EntrepriseAddressModel model)
        {
            int entrepriseAddressId = await _storageManager.AddAddress(_mapper.Map<EntrepriseAddress>(model));
            return entrepriseAddressId;
        }

        public async Task DeleteEntrepriseAddress(int id)
        {
            EntrepriseAddress entrepriseAddress = await _storageManager.GetAddress(id).ConfigureAwait(false);

            if (entrepriseAddress == null)
            {
                throw new KeyNotFoundException($"Entreprise Address Id: {id} not found.");
            }

            await _storageManager.DeleteAddress(entrepriseAddress);
        }

        public async Task UpdateEntrepriseAddress(int id, EntrepriseAddressModel model)
        {
            EntrepriseAddress entrepriseAddress = await _storageManager.GetAddress(id).ConfigureAwait(false);
            
            if (entrepriseAddress == null)
            {
                throw new KeyNotFoundException($"Entreprise Address Id: {id} not found.");
            }

            if (model.EntrepriseId < 1)
            {
                model.EntrepriseId = entrepriseAddress.EntrepriseId;
            }

            _mapper.Map<EntrepriseAddressModel, EntrepriseAddress>(model, entrepriseAddress);

            await _storageManager.UpdateAddress(entrepriseAddress);
        }

        public async Task<bool> CheckIfEntrepriseHasMainAddress(int entrepriseId)
        {
            List<EntrepriseAddress> entrepriseAddresses = await _storageManager.GetEntrepriseAddresses(entrepriseId).ConfigureAwait(false);

            if (entrepriseAddresses == null || entrepriseAddresses.Count == 0)
            {
                return false;
            }

            if (!entrepriseAddresses.Any(x=>x.IsMainAddress))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckIfEntrepriseHasMainAddressFromAddressId(int addressId)
        {
            EntrepriseAddress address = await _storageManager.GetAddress(addressId).ConfigureAwait(false);
            List<EntrepriseAddress> entrepriseAddresses = await _storageManager.GetEntrepriseAddresses(address.EntrepriseId).ConfigureAwait(false);

            if (entrepriseAddresses == null || entrepriseAddresses.Count == 0)
            {
                return false;
            }

            if (!entrepriseAddresses.Any(x => x.IsMainAddress))
            {
                return false;
            }

            return true;
        }

        public async Task<int> GetEntrepriseIdFromAdressId(int addressId)
        {
            EntrepriseAddress address = await _storageManager.GetAddress(addressId).ConfigureAwait(false);

            if (address == null)
            {
                throw new KeyNotFoundException($"Entreprise Address Id: {addressId} not found.");
            }

            return address.EntrepriseId;
        }
    }
}
