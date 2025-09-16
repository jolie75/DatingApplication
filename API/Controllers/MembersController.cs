using API.Entities;
using API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]  // mean that every Attribute if didn't receive authenticated, it will return error 401 unauthorized

    public class MembersController(IMemberRepository memberRepository) : BaseApiController
    {
        //Create endpoints 
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
        {
            return Ok(await memberRepository.GetMembersAsync());
        }

        //Now getting an individual HTTP request

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMembers(string id) //Localhost:5001/api/members/]Bob-id
        {
            var member = await memberRepository.GetMemberByIdAsync(id);

            if (member == null) return NotFound();

            return member;
        }

        [HttpGet("{id}/photos")]

        public async Task<ActionResult<IReadOnlyList<Photo>>> GeMemberPhotos(string id) //Localhost:5001/api/members/]Bob-id
        {
            return Ok(await memberRepository.GetPhotosForMemberAsync(id));
        }


    }
}