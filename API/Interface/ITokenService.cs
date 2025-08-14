using System;
using API.Interfaces;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }

}
