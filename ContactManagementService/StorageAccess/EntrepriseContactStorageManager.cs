using AutoMapper;
using ContactManagementService.Context;
using ContactManagementService.Entities;
using ContactManagementService.Models;
using ContactManagementService.StorageAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess
{
    public class EntrepriseContactStorageManager : IEntrepriseContactStorageManager
    {
        private readonly ContactManagementContext _context;
        private readonly IMapper _mapper;

        public EntrepriseContactStorageManager(ContactManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EntrepriseContactModel> AddEntrepriseContact(EntrepriseContact entrepriseContact)
        {
            await _context.EntrepriseContacts.AddAsync(entrepriseContact).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return _mapper.Map<EntrepriseContactModel>(entrepriseContact);
        }

        public async Task DeleteEntrepriseContact(EntrepriseContact entrepriseContact)
        {
            _context.EntrepriseContacts.Remove(entrepriseContact);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<EntrepriseContact> GetEntrepriseContact(int entrepriseId, int contactId)
        {
            return await _context.EntrepriseContacts.SingleOrDefaultAsync(x => x.ContactId == contactId && x.EntrepriseId == entrepriseId).ConfigureAwait(false);
        }

        public async Task UpdateEntrepriseContact(EntrepriseContact entrepriseContact)
        {
            _context.EntrepriseContacts.Update(entrepriseContact);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
