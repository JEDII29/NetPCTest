using NetPCTest.Data.Entities;
using NetPCTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Domain.Abstract
{
    public interface IContactService
    {
        Task<List<ContactEntity>> GetUserContacts(Guid userId);
        Task SaveNewContact(ContactEntity contact);
        Task EditUserContact(ContactEntity contact);
        Task DeleteUserContact(Guid contactId);
        Task<List<BasicContactModel>> GetUserBasicContacts(Guid userId);
    }
}
