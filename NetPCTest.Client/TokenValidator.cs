using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetPCTest.Client;
public class TokenValidator
{
    public bool IsJwtValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
                return false;

            var expiryDate = jwtToken.ValidTo;

            // Sprawdzenie czy token jest jeszcze ważny
            if (expiryDate != null && expiryDate >= DateTime.UtcNow)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas dekodowania tokenu JWT: {ex.Message}");
            return false;
        }
    }
    public string GetTokenId(string token)
    {
        string id = null;
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken != null)
            {
                id = jwtToken.Id;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wyciągania claimów z tokenu JWT: {ex.Message}");
        } 
        return id;
    }
}