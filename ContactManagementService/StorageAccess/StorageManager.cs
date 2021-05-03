using ContactManagementService.Context;
using ContactManagementService.Entities;
using ContactManagementService.StorageAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess
{
    public class StorageManager : IStorageManager
    {
        private readonly ContactManagementContext _context;

        public StorageManager(ContactManagementContext context)
        {
            _context = context;
        }

        public async Task<int> AddContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return contact.Id;
        }

        public async Task<Contact> GetContact(int id)
        {
            return await _context.Contacts.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task UpdateContact(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
