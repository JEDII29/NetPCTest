using Microsoft.EntityFrameworkCore;
using NetPCTest.Data;
using NetPCTest.Data.Entities;
using NetPCTest.Domain.Abstract;
using NetPCTest.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetPCTest.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UserEntity>> GetAllUsers()
        {
            var usersList = await _dbContext.Set<UserEntity>().ToListAsync();
            return usersList;
        }

        public async Task<List<UserModel>> GetUsersModels()
        {
            var usersList = await _dbContext.Set<UserEntity>().ToListAsync();
            List<UserModel> result = usersList.Select(u => new UserModel
            {
                Id = u.Id,
                Username = u.Login
            }).ToList();
            return result;
        }

        public async Task<Guid> GetUserId(string username) =>
            await _dbContext.Users
                .Where(x => x.Login == username)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            

    }
}
