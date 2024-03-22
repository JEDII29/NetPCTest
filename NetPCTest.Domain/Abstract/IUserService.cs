using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetPCTest.Data.Entities;
using NetPCTest.Domain.Model;

namespace NetPCTest.Domain.Abstract
{
    public interface IUserService
    {
        Task<List<UserEntity>> GetAllUsers();
        Task<Guid> GetUserId(string username);

        Task<List<UserModel>> GetUsersModels();
    }
}
