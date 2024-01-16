using Core.Entities;

namespace Core.Specifications;

public class ChatMembersWithFiltersForCountSpecification : BaseSpecification<ChatMember>
{
    public ChatMembersWithFiltersForCountSpecification(ChatMembersSpecParams chatMembersParams)       : base(x => 
        (!chatMembersParams.ChatRoomId.HasValue || x.ChatRoomId == chatMembersParams.ChatRoomId) && 
        (!chatMembersParams.UserId.HasValue || x.UserId == chatMembersParams.UserId)) 
    {
        
    }
}