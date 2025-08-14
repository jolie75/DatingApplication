using System;
using API.Interfaces;
using API.Entities;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Abstractions;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;


namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {

        var TokenKey = config["TokenKey"] ?? throw new Exception("Cannot get Token Key"); //sp you cannpt run the prgram without setting a token key

        if (TokenKey.Length < 64)
            throw new Exception("Token Key needs to be >=64 charachters");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenKey)); //Convert it into a SymmetricSecurityKey for token signing/validation.

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id)

        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var TokenDescriptor = new SecurityTokenDescriptor() // hold all the info need to create a token (a config object)
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        //Now create the Token
        var TokenHandler = new JwtSecurityTokenHandler();
        var token = TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(token);
    }

}