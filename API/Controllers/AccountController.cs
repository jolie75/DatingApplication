using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenservice) : BaseApiController
{
    
    [HttpPost("register")]  // api/account/register
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerdto)
    {
        if (await EmailExists(registerdto.Email)) return BadRequest("Email Taken");

        using var hmac = new HMACSHA512(); // A cryptography class to help us to hash a password

        var user = new AppUser
        {
            DisplayName = registerdto.DisplayName,
            Email = registerdto.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerdto.Password)),
            PasswodSalt = hmac.Key
        };

        context.Users.Add(user); // adding the user to the table now(Tell EF to track what's going on with this entity)
        await context.SaveChangesAsync(); // In order to save the user to our database(Then Save any changes to db)

        return user.ToDto(tokenservice);
    }



    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto) //getting our data out of our DB to confirm Login process
    {

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email); //using singleordefaultasync will now cause a problem if have a duplicate data so it prevent to return an exception error

        using var HMAC = new HMACSHA512(user.PasswodSalt);

        var ComputedHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < ComputedHash.Length; i++)
        {
            if (ComputedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }


        return user.ToDto(tokenservice);
    }


    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
    }
}
