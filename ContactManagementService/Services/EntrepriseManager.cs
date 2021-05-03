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
    public class EntrepriseManager : IEntrepriseManager
    {
        private readonly IEntrepriseStorageManager _storageManager;
        private readonly IMapper _mapper;

        public EntrepriseManager(IEntrepriseStorageManager entrepriseStorageManager, IMapper mapper)
        {
            _storageManager = entrepriseStorageManager;
            _mapper = mapper;
        }

        public async Task<int> AddEntreprise(EntrepriseModel model)
        {
            int entrepriseId = await _storageManager.AddEntreprise(_mapper.Map<Entreprise>(model));
            return entrepriseId;
        }

        public async Task DeleteEntreprise(int id)
        {
            Entreprise entreprise = await _storageManager.GetEntreprise(id).ConfigureAwait(false);

            if (entreprise == null)
            {
                throw new KeyNotFoundException($"Entreprise Id: {id} not found.");
            }

            await _storageManager.DeleteEntreprise(entreprise);
        }

        public async Task UpdateEntreprise(int id, EntrepriseModel model)
        {
            Entreprise entreprise = await _storageManager.GetEntreprise(id).ConfigureAwait(false);

            if (entreprise == null)
            {
                throw new KeyNotFoundException($"Entreprise Id: {id} not found.");
            }

            _mapper.Map<EntrepriseModel, Entreprise>(model, entreprise);

            await _storageManager.UpdateEntreprise(entreprise);
        }
    }
}
