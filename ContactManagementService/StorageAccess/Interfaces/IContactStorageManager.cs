using ContactManagementService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.StorageAccess.Interfaces
{
    public interface IContactStorageManager
    {
        Task<int> AddContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task<Contact> GetContact(int id);
        Task DeleteContact(Contact contact);
    }
}
