using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class MembersController(AppDbContext context) : BaseApiController
    {
        //Create endpoints and decorating them with attributes
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();

            return members;
        }

        //Now getting an individual HTTP request

        [Authorize]    // mean that every Attribute if didn't receive authenticated, it will return error 401 unauthorized
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetMembers(string id) //Localhost:5001/api/members/]Bob-id
        {
            var member = await context.Users.FindAsync(id);

            if (member == null) return NotFound();

            return member;



        }

    }
}