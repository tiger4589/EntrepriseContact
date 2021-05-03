using ContactManagementService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Context
{
    public class ContactManagementContext : DbContext
    {
        public ContactManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entreprise> Entreprises { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<EntrepriseAddress> EntrepriseAddresses { get; set; }
        public DbSet<EntrepriseContact> EntrepriseContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntrepriseContact>().HasKey(x => new { x.EntrepriseId, x.ContactId });
        }
    }
}
