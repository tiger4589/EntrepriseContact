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
    public class EntrepriseContactManager : IEntrepriseContactManager
    {
        private readonly IEntrepriseContactStorageManager _manager;
        private readonly IMapper _mapper;
        private readonly IEntrepriseStorageManager _entrepriseStorageManager;
        private readonly IContactStorageManager _contactStorageManager;

        public EntrepriseContactManager(IEntrepriseContactStorageManager manager, IMapper mapper, IEntrepriseStorageManager entrepriseStorageManager, IContactStorageManager contactStorageManager)
        {
            _manager = manager;
            _mapper = mapper;
            _entrepriseStorageManager = entrepriseStorageManager;
            _contactStorageManager = contactStorageManager;
        }

        public async Task<EntrepriseContactModel> AddEntrepriseContact(EntrepriseContactModel model)
        {
            Entreprise entreprise = await _entrepriseStorageManager.GetEntreprise(model.EntrepriseId).ConfigureAwait(false);

            if (entreprise == null)
            {
                throw new KeyNotFoundException($"Entreprise Id: {model.EntrepriseId} is not found");
            }

            Contact contact = await _contactStorageManager.GetContact(model.ContactId).ConfigureAwait(false);

            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact Id: {model.ContactId} is not found");
            }

            if (model.ContactType == Enums.EContactType.Freelancer && string.IsNullOrWhiteSpace(model.VATNumber))
            {
                throw new InvalidOperationException("A freelancer contact must have a VAT number");
            }

            EntrepriseContactModel resultModel = await _manager.AddEntrepriseContact(_mapper.Map<EntrepriseContact>(model)).ConfigureAwait(false);
            return resultModel;
        }

        public async Task DeleteEntrepriseContract(int entrepriseId, int contactId)
        {
            EntrepriseContact entrepriseContact = await _manager.GetEntrepriseContact(entrepriseId, contactId).ConfigureAwait(false);

            if (entrepriseContact == null)
            {
                throw new KeyNotFoundException($"Entreprise Id: {entrepriseId} and Contact Id: {contactId} combination is not found");
            }

            await _manager.DeleteEntrepriseContact(entrepriseContact).ConfigureAwait(false);
        }

        public async Task UpdateEntrepriseContact(EntrepriseContactModel model)
        {
            EntrepriseContact entrepriseContact = await _manager.GetEntrepriseContact(model.EntrepriseId, model.ContactId).ConfigureAwait(false);

            if (entrepriseContact == null)
            {
                throw new KeyNotFoundException($"Entreprise Id: {model.EntrepriseId} and Contact Id: {model.ContactId} combination is not found");
            }

            if (model.ContactType == Enums.EContactType.Freelancer && string.IsNullOrWhiteSpace(model.VATNumber))
            {
                throw new InvalidOperationException("A freelancer contact must have a VAT number");
            }

            _mapper.Map<EntrepriseContactModel, EntrepriseContact>(model, entrepriseContact);

            await _manager.UpdateEntrepriseContact(entrepriseContact).ConfigureAwait(false);
        }
    }
}
