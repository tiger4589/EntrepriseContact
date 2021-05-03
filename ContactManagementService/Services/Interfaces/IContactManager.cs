using ContactManagementService.Entities;
using ContactManagementService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementService.Services.Interfaces
{
    public interface IContactManager
    {
        Task<int> AddContact(ContactModel model);
        Task UpdateContact(int id, ContactModel model);
        Task DeleteContact(int id);
    }
}
