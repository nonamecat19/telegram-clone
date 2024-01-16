using API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ChatMembersController(IGenericRepository<ChatMember> chatMembersRepo) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<Pagination<ChatMember>>> GetChatMembers([FromQuery]ChatMembersSpecParams chatMembersParams)
    {
        var spec = new ChatMembersWithChatRoomAndUsersSpecification(chatMembersParams);
        return Ok(await chatMembersRepo.ListAsync(spec));
    }
}