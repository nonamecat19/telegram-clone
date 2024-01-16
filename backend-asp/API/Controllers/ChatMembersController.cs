using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ChatMembersController(IGenericRepository<ChatMember> chatMembersRepo) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ChatMember>>> GetChatMembers()
    {
        var spec = new ChatMembersWithChatRoomAndUsersSpecification();
        return Ok(await chatMembersRepo.ListAsync(spec));
    }
    
    [HttpGet("chatRoom/{chatRoomId:int}")]
    public async Task<ActionResult<List<ChatMember>>> GetChatMembersByRoomId(int chatRoomId)
    {
        var spec = new ChatMembersWithChatRoomAndUsersSpecification(chatRoomId);
        return Ok(await chatMembersRepo.GetEntityWithSpec(spec));
    }
}