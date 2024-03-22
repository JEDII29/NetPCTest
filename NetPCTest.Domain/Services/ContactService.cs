using NetPCTest.Domain.Abstract;
using NetPCTest.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPCTest.Data.Entities;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Domain.Model;

namespace NetPCTest.Domain.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _dbContext;

        public ContactService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteUserContact(Guid contactId)
        {
            var itemToDelete = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
            if (itemToDelete != null)
            {
                _dbContext.Contacts.Remove(itemToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public Task EditUserContact(ContactEntity contact)
        {
            _dbContext.Contacts.Update(contact);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<List<ContactEntity>> GetUserContacts(Guid userId)
        {
            var contactsList = await _dbContext.Contacts.Where(x => x.UserId == userId).ToListAsync();
            return contactsList;
        }
        public async Task<List<BasicContactModel>> GetUserBasicContacts(Guid userId)
        {
            List<BasicContactModel> contactsList = await _dbContext.Contacts.Where(x => x.UserId == userId)
                .Select(x => new BasicContactModel { Firstname = x.Firstname, Lastname = x.Surname })
                .ToListAsync();
            return contactsList;
        }

        public async Task SaveNewContact(ContactEntity contact)
        {
            await _dbContext.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
        }
    }
}
