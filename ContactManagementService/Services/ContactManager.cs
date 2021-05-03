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
    public class ContactManager : IContactManager
    {
        private readonly IContactStorageManager _storageManager;
        private readonly IMapper _mapper;

        public ContactManager(IContactStorageManager storageManager, IMapper mapper)
        {
            _storageManager = storageManager;
            _mapper = mapper;
        }

        public async Task<int> AddContact(ContactModel model)
        {
            int contactId = await _storageManager.AddContact(_mapper.Map<Contact>(model));
            return contactId;
        }

        public async Task DeleteContact(int id)
        {
            Contact contact = await _storageManager.GetContact(id).ConfigureAwait(false);

            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact Id: {id} not found.");
            }

            await _storageManager.DeleteContact(contact);
        }

        public async Task UpdateContact(int id, ContactModel model)
        {
            Contact contact = await _storageManager.GetContact(id).ConfigureAwait(false);

            if (contact == null)
            {
                throw new KeyNotFoundException($"Contact Id: {id} not found.");
            }

            _mapper.Map<ContactModel, Contact>(model, contact);

            await _storageManager.UpdateContact(contact);
        }
    }
}
