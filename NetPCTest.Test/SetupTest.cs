using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Data;
using NetPCTest.Data.Entities;
using NetPCTest.Domain.Abstract;
using Xunit;

namespace MyProject.Tests
{
    public class SetupTest : IDisposable
    {
        private readonly AppDbContext _dbContext;
        private readonly IContactService _contactService;
        public SetupTest(AppDbContext dbContext, IContactService contactService)
        {
            _dbContext = dbContext;
            _contactService = contactService;
        }

        [Fact]
        public async Task Should_Clear_And_Populate_Database_With_Users_And_Contacts()
        {
            // Wyczyść bazę danych
            await _dbContext.Database.EnsureDeletedAsync();
            await _dbContext.Database.EnsureCreatedAsync();

            // Dodaj kilka rekordów do tabel Users i Contacts
            var user1 = new UserEntity {
                Id = new Guid(),
                Login = "Zdzislaw",
                PasswordHash = "haslo"
            };
            var user2 = new UserEntity
            {
                Id = new Guid(),
                Login = "Leszek",
                PasswordHash = "haslo"
            };

            _dbContext.Users.AddRange(user1, user2);
            await _dbContext.SaveChangesAsync();

            // Sprawdź, czy rekordy zostały dodane poprawnie
            var savedUser1 = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == "Zdzislaw");
            var savedUser2 = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == "Leszek");


            Assert.NotNull(savedUser1);
            Assert.NotNull(savedUser2);
 
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}