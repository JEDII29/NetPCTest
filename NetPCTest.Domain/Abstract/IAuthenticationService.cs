using NetPCTest.Data.Entities;

namespace NetPCTest.Domain.Abstract
{
    public interface IAuthenticationService
    {
        Task<string> GenerateAccessToken(string username);
        bool IsValidUser(string username, string password);
    }
}
