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
    public class EntrepriseStorageManager : IEntrepriseStorageManager
    {
        private readonly ContactManagementContext _context;

        public EntrepriseStorageManager(ContactManagementContext context)
        {
            _context = context;
        }

        public async Task<int> AddEntreprise(Entreprise entreprise)
        {
            await _context.Entreprises.AddAsync(entreprise).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entreprise.Id;
        }

        public async Task DeleteEntreprise(Entreprise entreprise)
        {
            _context.Entreprises.Remove(entreprise);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Entreprise> GetEntreprise(int id)
        {
            return await _context.Entreprises.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task UpdateEntreprise(Entreprise entreprise)
        {
            _context.Entreprises.Update(entreprise);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
