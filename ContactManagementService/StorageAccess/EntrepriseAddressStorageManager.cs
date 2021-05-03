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
    public class EntrepriseAddressStorageManager : IEntrepriseAddressStorageManager
    {
        private readonly ContactManagementContext _context;

        public EntrepriseAddressStorageManager(ContactManagementContext context)
        {
            _context = context;
        }

        public async Task<int> AddAddress(EntrepriseAddress address)
        {
            await _context.EntrepriseAddresses.AddAsync(address).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return address.Id;
        }

        public async Task DeleteAddress(EntrepriseAddress address)
        {
            _context.EntrepriseAddresses.Remove(address);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<EntrepriseAddress> GetAddress(int id)
        {
            return await _context.EntrepriseAddresses.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task UpdateAddress(EntrepriseAddress address)
        {
            _context.EntrepriseAddresses.Update(address);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<EntrepriseAddress>> GetEntrepriseAddresses(int entrepriseId)
        {
            return await _context.EntrepriseAddresses.Where(x=>x.EntrepriseId == entrepriseId).ToListAsync().ConfigureAwait(false);
        }
    }
}
